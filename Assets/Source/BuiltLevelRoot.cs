using Cinemachine;
using Source.Characters;
using Source.Characters.Behaviour;
using Source.Characters.Train;
using Source.Characters.Train.Payment;
using Source.Characters.Train.TrainCar;
using Source.Characters.Upgrades;
using Source.Characters.Worker;
using Source.Characters.Worker.Merge;
using Source.Characters.Worker.Merge.GenericMerge;
using Source.GlobalMap;
using Source.Map;
using Source.Map.ChunksLoader;
using Source.Map.ChunksLoader.MapLoader;
using Source.Map.ChunksLoader.ObjectsGeneration;
using Source.Map.ChunksLoader.ObjectsGeneration.Environment;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsProvider;
using Source.Map.InteractableObjects;
using Source.Map.InteractableObjects.Cells;
using Source.Map.InteractableObjects.Priority;
using Source.Progress;
using Source.Stack;
using Source.UI;
using Source.UI.UpdatableView;
using UnityEngine;
using ICoroutineRunner = Source.Common.ICoroutineRunner;

namespace Source
{
    class BuiltLevelRoot : BaseCompositionRoot, ICoroutineRunner
    {
        [SerializeField] private ScriptableMapConfig _locationConfig;
        [SerializeField] private MapChunk _startChunkTemplate;
        [SerializeField] private TrainCompleted _trainTemplate;
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private DiscSamplingGenerator _environmentPointsProvider;
        [SerializeField] private DiscSamplingGenerator _resourcePointsProvider;
        [SerializeField] private DistanceUnlockablesList _distanceUnlocksList;
        [SerializeField] private WorkersList _workersList;
        [SerializeField] private TrainCarList _trainCarList;
        [SerializeField] private TrainSpeedProgression _trainSpeedProgression;
        [SerializeField] private LocationType _location;
        [SerializeField] private RandomStackableFactory _stackableFactory;
        [SerializeField] private LevelTutorialFinish _tutorialFinish;
        [SerializeField] private StackableTypes _spawnableStackables;

        private MapUpdater _mapUpdater;
        private TrainCompleted _train;
        private UpgradableMerger<WorkerStats, Worker> _upgradableMerger;
        private UpgradableMerger<TrainCarStats, TrainCar> _trainCarMerger;
        private OfflineIncomeHelper _offlineIncomeHelper;
        private DistanceSaver _distanceSaver;
        private CompleteTrainCamera _camera;
        private CollectedResourcesSell _sell;

        protected override void InitializeLevel()
        {
            _camera = FindObjectOfType<CompleteTrainCamera>();

            _train = Instantiate(_trainTemplate);

            if (_tutorialFinish != null)
                _tutorialFinish.Construct(_train.TrainMovement, LevelData);

            _distanceSaver = new DistanceSaver(LevelData.CompleteLevelData.TraveledDistance, _train.transform);

            InteractableObjectsContainer railwayContainer = new InteractableObjectsContainer();
            ResourceContainer resourceContainer = new ResourceContainer(railwayContainer, StackableTypes.All());

            IInteractablesContainer resourceInitializingContainer = new InteractablesInitializingContainer(
                resourceContainer, new ConstPriority(1));

            UpgradeLevel[] upgradeLevels = LevelData.CompleteLevelData.UpgradeLevels;
            UpgradeLevel mergeUpgradeLevel = LevelData.CompleteLevelData.MergeUpgradeLevel;

            UpgradeValue<float> speedUpgradeValue = new(new UpgradeLevel(1), _trainSpeedProgression.SpeedProgression);
            _train.TrainMovement.Construct(speedUpgradeValue, LevelSettings.Length);

            ResourceCapacityValueFactory valueFactory =
                new ResourceCapacityValueFactory(LevelData.CompleteLevelData.UpgradeLevels[1]);
            ResourceCostFactory costFactory =
                new ResourceCostFactory(LevelData.CompleteLevelData.UpgradeLevels[0]);
            IncomeFactory incomeFactory =
                new IncomeFactory(LevelData.CompleteLevelData.UpgradeLevels[2]);

            UpgradeValue<int> capacityUpgrade = valueFactory.Create();
            UpgradeValue<float> costUpgrade = costFactory.Create();

            LocationConfig firstLocation = _locationConfig.LocationConfigs[0];
            int distance = firstLocation.Length * firstLocation.ChunkWidth;
            _offlineIncomeHelper =
                new OfflineIncomeHelper(LevelData.CompleteLevelData, costUpgrade, capacityUpgrade,
                    incomeFactory.Create());

            CarsHolder carsHolder = InitializeCarsMerge(railwayContainer, upgradeLevels, mergeUpgradeLevel);
            _sell = new CollectedResourcesSell(carsHolder, costUpgrade, _gameUI.PayView);

            _gameUI.SellButton.Construct(_sell);

            _train.transform.position = Vector3.right * 30;

            ICanBuyCondition[] canBuyConditions =
            {
                new CanAlwaysBuy(), new CarsAmountUpgradeLevelCondition(carsHolder, 7), new CanAlwaysBuy()
            };

            OfflineIncomeCollectedCondition incomeCondition = new(ProgressService.GameData.LevelsData, LevelData);
            incomeCondition.InitializeOfflineIncome();
            IUpdateCondition updateCondition = new TimeElapsedCondition(LevelTimer, 90, incomeCondition);

            _gameUI.Construct(MoneyBalance, upgradeLevels, 2, mergeUpgradeLevel, LevelLoader, _location,
                canBuyConditions, updateCondition, _train.TrainMovement);

            foreach (WorkerStats stats in _workersList.WorkerStats)
                stats.Construct(upgradeLevels[2]);

            InitializeWorkersMerge(resourceContainer, LevelData.UpgradeLevels, railwayContainer);
            InitializeMapGeneration(resourceInitializingContainer);
        }

        private CarsHolder InitializeCarsMerge(InteractableObjectsContainer railwayContainer,
            UpgradeLevel[] upgradeLevels,
            UpgradeLevel mergeUpgradeLevel)
        {
            CarsHolder carsHolder = new CarsHolder(_train.CarsRoot,
                new CinemachineCameraTargetGroupInit(new ObjectsContainer<TrainCar>(), _camera.TargetGroup));
            IMergeableFactory<TrainCarStats, TrainCar> mergeableFactory =
                new TrainCarFactory(_trainCarList, carsHolder, railwayContainer);
            IMergeView<TrainCar> mergeView = new TrainCarMergeView(carsHolder, railwayContainer, this);

            _trainCarMerger = new UpgradableMerger<TrainCarStats, TrainCar>(
                new StraightIntUpgradeValue(upgradeLevels[1]), mergeUpgradeLevel,
                new Merger<TrainCarStats, TrainCar>(_trainCarList.TrainCarStats, mergeableFactory, mergeView));
            _trainCarMerger.Start();

            TrainLoader trainLoader = new TrainLoader(carsHolder, _stackableFactory, _offlineIncomeHelper);
            trainLoader.Load();

            return carsHolder;
        }

        private void InitializeWorkersMerge(ResourceContainer resourceContainer, UpgradeLevel[] upgradeLevels,
            InteractableObjectsContainer railwayContainer)
        {
            MergeableWorkerFactory mergeableWorkerFactory = new MergeableWorkerFactory(railwayContainer,
                resourceContainer, _workersList,
                _train.SpawnPoint, new CellWay(), _train.WayPoints);

            _upgradableMerger = new UpgradableMerger<WorkerStats, Worker>(
                new UpgradeValue<int>(new UpgradeLevel(5), new LevelIntProgression(0, 1)),
                new UpgradeLevel(0),
                new Merger<WorkerStats, Worker>(_workersList.WorkerStats, mergeableWorkerFactory, new NullMergeView()));

            _upgradableMerger.Start();
        }

        private void InitializeMapGeneration(IInteractablesContainer resourceInitializingContainer)
        {
            ObjectsContainer<ObjectWithBounds> container = new ObjectsContainer<ObjectWithBounds>();

            IChunkFactory chunkFactory =
                new BuiltChunkFactory(resourceInitializingContainer, _location, _spawnableStackables);
            IChunkLoader<MapChunk> chunkLoader = new ChunkLoaderObjectGeneratorDecorator(
                new MapObjectGeneratorComposite(
                    new MapObjectGenerator(new ObjectGenerator<ObjectWithBounds>(_environmentPointsProvider,
                        new EnvironmentObjectFactory(_distanceUnlocksList.DistanceUnlockables), container)),
                    new ResourcesGenerator(
                        new ObjectGenerator<Resource>(_resourcePointsProvider,
                            new ResourceFactory(_locationConfig, resourceInitializingContainer, _location,
                                _spawnableStackables),
                            new ObjectsContainer<Resource>()), container)),
                new ChunkLoader(_locationConfig, chunkFactory));

            MapLoader<MapChunk> mapLoader =
                new MapLoader<MapChunk>(new MapConfig(_locationConfig.LocationConfigs), chunkLoader, 25, 20);

            _mapUpdater = new MapUpdater(_train.transform, mapLoader);
            _mapUpdater.LoadFakeStartChunk(_startChunkTemplate, _location, _train.transform.position.x);
        }

        private void Update()
        {
            _mapUpdater.UpdateMap();
            _trainCarMerger.Update();
            _upgradableMerger.Update();
            _distanceSaver.Save();
            _offlineIncomeHelper.UpdateLastClaimForNormalizedProgress(_sell.NormalizedProgress);
            SaveLoadService.Save();
        }
    }
}

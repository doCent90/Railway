using System.Linq;
using Source.Analytics;
using Source.Characters;
using Source.Characters.Behaviour;
using Source.Characters.Train;
using Source.Characters.Train.Payment;
using Source.Characters.Upgrades;
using Source.Characters.Worker;
using Source.Characters.Worker.Merge;
using Source.Characters.Worker.Merge.GenericMerge;
using Source.Common;
using Source.Map;
using Source.Map.ChunksLoader;
using Source.Map.ChunksLoader.MapLoader;
using Source.Map.ChunksLoader.ObjectsGeneration;
using Source.Map.ChunksLoader.ObjectsGeneration.Environment;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsProvider;
using Source.Map.InteractableObjects;
using Source.Map.InteractableObjects.Cells;
using Source.Map.InteractableObjects.Payment;
using Source.Map.InteractableObjects.Priority;
using Source.Progress;
using Source.SaveLoad.Timer;
using Source.Stack;
using Source.Tutorials;
using Source.UI;
using Source.UI.UpdatableView;
using UnityEngine;

namespace Source
{
    public class BuildingLevelRoot : BaseCompositionRoot, ICoroutineRunner
    {
        [SerializeField] private GameUI _gameUI;
        [SerializeField] private ScriptableLevelFloatProgression _incomeProgression;
        [SerializeField] private TrainStarted _trainTemplate;
        [SerializeField] private DynamicCamera _dynamicCamera;
        [SerializeField] private WorkersList _workersList;
        [SerializeField] private ObjectContainerInteractableProvider _containerInteractableProvider;

        [Header("MapGeneration")] [SerializeField]
        private ScriptableMapConfig _locationConfig;

        [SerializeField] private MapChunk _startChunkTemplate;
        [SerializeField] private LocationType _location;
        [SerializeField] private DiscSamplingGenerator _environmentPointsProvider;
        [SerializeField] private DiscSamplingGenerator _resourcePointsProvider;
        [SerializeField] private DistanceUnlockablesList _distanceUnlocksList;
        [SerializeField] private int _mapNotDestroyedBackBuffer = 15;

        private float _lastClick;
        private UpgradableMerger<WorkerStats, Worker> _upgradableMerger;
        private MapUpdater _mapUpdater;
        private AnalyticsPlayTimeLogger _analyticsPlayTimeLogger;
        private CurrentDistanceTaskUpdate _currentDistanceTaskUpdate;
        private DistancePayer _distancePayer;
        private ChunkFactory _chunkFactory;
        private DistanceSaver _distanceSaver;
        private CellWay _cellWay;
        private ILevelFinish _buildingLevelFinish;
        private ITutorialRunner _tutorialRunner;

        protected override void InitializeLevel()
        {
            UpgradeLevel trainUpgrade = LevelData.TrainUpgrade;
            _cellWay = new CellWay();

            _trainTemplate = Instantiate(_trainTemplate);
            _trainTemplate.GetComponentInChildren<TrainUpgradesView>().Construct(trainUpgrade);
            _trainTemplate.TrainMovement.Construct(_cellWay);
            _dynamicCamera.Construct(_trainTemplate.TrainMovement);

            _distanceSaver = new DistanceSaver(LevelData.TraveledDistance, _trainTemplate.transform);

            _currentDistanceTaskUpdate =
                new CurrentDistanceTaskUpdate(LevelData.TraveledDistance, _gameUI.TaskView, this,
                    LevelData.DistanceTasks);
            _currentDistanceTaskUpdate.Start();

            _buildingLevelFinish = new BuildingLevelFinish(LevelLoader, LevelData);

            ILevelFinish levelFinish =
                new SendEventLevelFinish(Analytics, ProgressService.GameData, LevelTimer, _buildingLevelFinish);

            _tutorialRunner = new TutorialRunner();
            _tutorialRunner.Run(_gameUI.TapTutorial);
            BuildableStationInitializer stationInitializer =
                new BuildableStationInitializer(_tutorialRunner, _gameUI, _dynamicCamera,
                    levelFinish, _gameUI.GlobalMoneyPayView);

            InteractableObjectsContainer railwayContainer = new InteractableObjectsContainer();
            ResourceContainer resourceContainer = new ResourceContainer(railwayContainer, StackableTypes.All());

            _containerInteractableProvider.Construct(railwayContainer);

            IInteractablesContainer resourceInitializingContainer = new InteractablesInitializingContainer(
                resourceContainer, new ConstPriority(1));

            _chunkFactory = new ChunkFactory(resourceInitializingContainer, _tutorialRunner,
                new InteractablesInitializingContainer(railwayContainer, new MultiplyNumberByConst(2)), _cellWay,
                stationInitializer, _location);

            IChunkFactory chunkFactory = new CompositeChunkFactory(
                _chunkFactory,
                stationInitializer,
                new ChunkTutorialInitializer(_tutorialRunner),
                new PaymentForActionInitializer(_gameUI.PayView)
            );

            InitializeMapGeneration(resourceInitializingContainer, chunkFactory);

            int level = ProgressService.GameData.LevelsData.Level;
            var upgradeLevelData = ProgressService.GameData.LevelsData.Levels[level - level % 3];

            UpgradeLevel[] upgradeLevels = upgradeLevelData.UpgradeLevels;
            UpgradeLevel mergeUpgradeLevel = upgradeLevelData.MergeUpgradeLevel;

            ICanBuyCondition[] canBuyConditions = {new CanAlwaysBuy(), new CanAlwaysBuy(), new CanAlwaysBuy()};

            OfflineIncomeCollectedCondition incomeCondition = new(ProgressService.GameData.LevelsData, LevelData);
            incomeCondition.InitializeOfflineIncome();
            IUpdateCondition updateCondition = new TimeElapsedCondition(LevelTimer, 90, incomeCondition);

            _gameUI.Construct(MoneyBalance, upgradeLevels, _workersList.WorkerConfigs.Count() - 1, mergeUpgradeLevel,
                LevelLoader, _location, canBuyConditions, updateCondition);

            foreach (WorkerStats stats in _workersList.WorkerStats)
                stats.Construct(upgradeLevels[2]);

            InitializeWorkersMerge(railwayContainer, resourceContainer, upgradeLevels, mergeUpgradeLevel);

            _distancePayer = new DistancePayer(_trainTemplate.transform,
                new UpgradeValue<float>(upgradeLevels[0], _incomeProgression.IncomeProgression), _gameUI.PayView,
                new FixedPayAmountProvider(18));
        }

        private void InitializeWorkersMerge(InteractableObjectsContainer railwayContainer,
            ResourceContainer resourceContainer,
            UpgradeLevel[] upgradeLevels, UpgradeLevel mergeUpgradeLevel)
        {
            MergeableWorkerFactory mergeableWorkerFactory = new MergeableWorkerFactory(railwayContainer,
                resourceContainer, _workersList,
                _trainTemplate.SpawnPoint, _cellWay, _trainTemplate.WayPoints);

            WorkerMergeView workerMergeView = new WorkerMergeView(_gameUI, _dynamicCamera, _trainTemplate.MergeParticles,
                _trainTemplate.SpawnPoint);

            _upgradableMerger = new UpgradableMerger<WorkerStats, Worker>(new StraightIntUpgradeValue(upgradeLevels[1]),
                mergeUpgradeLevel,
                new Merger<WorkerStats, Worker>(_workersList.WorkerStats, mergeableWorkerFactory, workerMergeView));

            _upgradableMerger.Start();
        }

        private void InitializeMapGeneration(IInteractablesContainer resourceContainer, IChunkFactory chunkFactory)
        {
            ObjectsContainer<ObjectWithBounds> container = new ObjectsContainer<ObjectWithBounds>();

            IChunkLoader<MapChunk> chunkLoader = new ChunkLoaderObjectGeneratorDecorator(
                new MapObjectGeneratorComposite(
                    new MapObjectGenerator(new ObjectGenerator<ObjectWithBounds>(_environmentPointsProvider,
                        new EnvironmentObjectFactory(_distanceUnlocksList.DistanceUnlockables), container)),
                    new ResourcesGenerator(
                        new ObjectGenerator<Resource>(_resourcePointsProvider,
                            new ResourceFactory(_locationConfig, resourceContainer, _location,
                                StackableTypes.All()),
                            new ObjectsContainer<Resource>()), container)),
                new ChunkLoader(_locationConfig, chunkFactory));

            MapLoader<MapChunk> mapLoader =
                new(new MapConfig(_locationConfig.LocationConfigs), chunkLoader, _mapNotDestroyedBackBuffer, 20);

            _mapUpdater = new MapUpdater(_trainTemplate.transform, mapLoader);

            float traveledDistance = LevelData.TraveledDistance.Distance;
            float spawnX = mapLoader.GetClosestChunkEnd(traveledDistance);
            _mapUpdater.LoadFakeStartChunk(_startChunkTemplate, _location, spawnX);

            if (traveledDistance == 0)
                spawnX -= 7;

            _trainTemplate.transform.position = Vector3.right * spawnX;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                _buildingLevelFinish.Finish();

            _tutorialRunner.Update();
            SaveLoadService.Save();
            _currentDistanceTaskUpdate.Update();
            _distancePayer.Update();
            _mapUpdater.UpdateMap();
            _upgradableMerger.Update();
            _distanceSaver.Save();
            _chunkFactory.Update();
        }
    }
}

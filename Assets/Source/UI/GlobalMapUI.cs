using Source.LevelLoaderService;
using Source.Characters.Upgrades;
using Source.GlobalMap;
using Source.SaveLoad;
using Source.Common;
using Source.Money;
using Source.Analytics;
using UnityEngine;

namespace Source.UI
{
    public class GlobalMapUI : MonoBehaviour, ICoroutineRunner, IGlobalMapUI
    {
        [SerializeField] private GlobalButtonsHolder[] _globalButtonsHolder;

        private GlobalMoneyPayView _moneyGlobalPayView;
        private LoadingScreen _loadingScreen;
        private RegionsHolder _regionsHolder;
        private MoneyBalance _moneyBalance;
        private MoneyView _moneyView;
        private LevelsData _levelsData;
        private Camera _camera;
        private IAnalyticsService _analyticsService;
        private ICurrentTrainView _currentTrainView;
        private bool _hasLoadStarted = false;
        private IEnvironmentPayer _payView => _moneyGlobalPayView;

        private void OnDisable()
        {
            for (int i = 0; i < _globalButtonsHolder.Length; i++)
            {
                for (int y = 0; y < _globalButtonsHolder[i].GlobalProgressUIs.Count; y++)
                    _globalButtonsHolder[i].GlobalProgressUIs[y].ButtonsUI.UnSubscribeButtons();
            }
        }

        public void Construct(RegionsHolder regionsHolder, GlobalMoneyPayView moneyGlobalPayView, MoneyView moneyView,
            MoneyBalance moneyBalance, IProgressService progressService, ILevelLoader levelLoader,
            Transform moneyPosition, LoadingScreen loadingScreen, ICurrentTrainView currentTrainView,
            IAnalyticsService analyticsService, Camera camera)
        {
            _regionsHolder = regionsHolder;
            _moneyGlobalPayView = moneyGlobalPayView;
            _camera = camera;
            _moneyView = moneyView;
            _moneyBalance = moneyBalance;
            _levelsData = progressService.GameData.LevelsData;
            _loadingScreen = loadingScreen;
            _currentTrainView = currentTrainView;
            _analyticsService = analyticsService;

            _moneyView.Construct(_moneyBalance);
            _moneyGlobalPayView.Construct(_moneyBalance, moneyPosition);

            Initialize(progressService.GameData.LevelsData, levelLoader);
        }

        public void OnLoadStarted() => _hasLoadStarted = true;

        private void Initialize(LevelsData levelsData, ILevelLoader levelLoader)
        {
            int totalLevel = 0;

            for (int i = 0; i < _globalButtonsHolder.Length; i++)
            {
                foreach (GlobalProgressUI progressUI in _globalButtonsHolder[i].GlobalProgressUIs)
                {
                    LevelData level = levelsData.Levels[totalLevel];

                    ResourceCapacityValueFactory capacityFactory =
                        new ResourceCapacityValueFactory(level.CompleteLevelData.UpgradeLevels[1]);

                    ResourceCostFactory costFactory =
                        new ResourceCostFactory(level.CompleteLevelData.UpgradeLevels[0]);

                    IncomeFactory incomeFactory =
                        new IncomeFactory(level.CompleteLevelData.UpgradeLevels[2]);

                    UpgradeValue<int> capacity = capacityFactory.Create();
                    UpgradeValue<float> cost = costFactory.Create();
                    UpgradeValue<float> incomeUpgradeValue = incomeFactory.Create();

                    AccumulatedIncomeProvider incomeProvider =
                        new AccumulatedIncomeProvider(cost, capacity);

                    OfflineIncomeHelper offlineIncomeHelper =
                        new(level.CompleteLevelData, cost, capacity, incomeUpgradeValue);

                    progressUI.GlobalProgressView.Construct(level, offlineIncomeHelper, _levelsData,
                        _regionsHolder.Regions[totalLevel], totalLevel, incomeProvider, _currentTrainView, _camera);

                    progressUI.ButtonsUI.Construct(capacity, _payView, levelLoader, totalLevel,
                        offlineIncomeHelper, _loadingScreen, _analyticsService, this, progressUI.GlobalProgressView, this);

                    progressUI.ButtonsUI.SubscribeButtons();

                    totalLevel++;
                }
            }
        }

        private void Update()
        {
            if (_hasLoadStarted)
                return;

            foreach (GlobalButtonsHolder buttonHolder in _globalButtonsHolder)
            {
                foreach (GlobalProgressUI progressUI in buttonHolder.GlobalProgressUIs)
                    progressUI.GlobalProgressView.Update();
            }
        }
    }
}

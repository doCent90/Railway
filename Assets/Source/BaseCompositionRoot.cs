using DG.Tweening;
using Source.Analytics;
using Source.Analytics.Timer;
using Source.LevelLoaderService;
using Source.Map;
using Source.Money;
using Source.SaveLoad;
using UnityEngine;

namespace Source
{
    [DefaultExecutionOrder(-1)]
    public abstract class BaseCompositionRoot : MonoBehaviour
    {
        [SerializeField] private ScriptableGameDataFactory _gameDataFactory;

        protected ISaveLoadService SaveLoadService;
        protected IProgressService ProgressService;
        protected IAnalyticsService Analytics;
        protected ILevelLoader LevelLoader;
        protected ILevelLoader BaseLevelLoader;
        protected LevelData LevelData;
        protected MoneyBalance MoneyBalance;
        protected LevelTimer LevelTimer = new LevelTimer();

        private ITimerService Timer;

        public void Construct(ILevelLoader levelLoader, IAnalyticsService analytics, ITimerService timer,
            IProgressService progressService, ISaveLoadService saveLoadService)
        {
            SaveLoadService = saveLoadService;
            ProgressService = progressService;
            Timer = timer;
            Analytics = analytics;
            BaseLevelLoader = levelLoader;
        }

        private void Start()
        {
            InitializeInfrastructure();
            InitializeLevel();
        }

        protected abstract void InitializeLevel();

        private void InitializeInfrastructure()
        {
            Analytics ??= new NullAnalytics();
            ProgressService ??= new ProgressService();
            SaveLoadService ??= new SaveLoadService(ProgressService);

            if (ProgressService.GameData == null)
            {
                ProgressService.GameData = SaveLoadService.Load() ?? _gameDataFactory.Create();
                SaveLoadService.Save();
                ProgressService.GameData = SaveLoadService.Load();
            }

            LevelData = ProgressService.GameData.LevelsData.Levels[ProgressService.GameData.LevelsData.Level];

            Timer ??= new Timer();
            BaseLevelLoader ??= new LevelLoader(new SceneLoader(), ProgressService, new ScenesConfiguration());
            LevelLoader = new LevelLoadEventSender(ProgressService.GameData, Analytics, LevelTimer, BaseLevelLoader);
            MoneyBalance = ProgressService.GameData.MoneyBalance;
        }

        private void OnDestroy()
        {
            DOTween.KillAll();
        }
    }
}

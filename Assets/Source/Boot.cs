using FreeplaySDK;
using Source.Analytics;
using Source.Analytics.AppMetricaAnalytics;
using Source.Analytics.Timer;
using Source.LevelLoaderService;
using Source.Map;
using Source.SaveLoad;
using UnityEngine;

namespace Source
{
    public class Boot : MonoBehaviour
    {
        [SerializeField] private ScriptableGameDataFactory _gameDataFactory;
        [SerializeField] private ScenesConfiguration _scenesConfiguration;

        private readonly Timer _playTimeTimer = new Timer();
        private readonly Timer _timeSpentTimer = new Timer();
        private UserPlaytime _userPlaytime;
        private AnalyticsPlayTimeLogger _analyticsPlayTimeLogger;
        private bool _initialized;
        private IAnalyticsService _analytics;
        private ILevelLoader _levelLoader;
        private IProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        private void OnEnable() =>
            Freeplay.OnInitialized += Initialize;

        private void OnDisable() =>
            Freeplay.OnInitialized -= Initialize;

        private void Initialize()
        {
            _progressService = new ProgressService();
            _saveLoadService = new SaveLoadService(_progressService);
            _progressService.GameData = _saveLoadService.Load() ?? _gameDataFactory.Create();
            _saveLoadService.Save();
            _progressService.GameData = _saveLoadService.Load();

            IYandexAppMetrica yandexAppMetrica = AppMetrica.Instance;

            AppMetricaProfile profile =
                new AppMetricaProfile(_progressService.GameData.AnalyticsData, yandexAppMetrica);
            profile.InitAppMetricaProfile();

            _userPlaytime = new UserPlaytime(_playTimeTimer, _progressService.GameData.AnalyticsData);

            _analytics = new AnalyticsComposite(new AppMetricaAnalytics(yandexAppMetrica, _userPlaytime),
                new FreeplayAnalyticsService());

            _analyticsPlayTimeLogger = new AnalyticsPlayTimeLogger(_userPlaytime, _analytics);

            _levelLoader = new NotifyLevelLoad(this,
                new LevelLoader(new SceneLoader(), _progressService, _scenesConfiguration));

            LevelLoadEventSender levelLoader = new LevelLoadEventSender(_progressService.GameData, _analytics,
                new LevelTimer(),
                new NotifyLevelLoad(this, _levelLoader));

            if (_progressService.GameData.LevelsData.MenuOpened)
                levelLoader.LoadMenu();
            else
                levelLoader.LoadLevel(_progressService.GameData.LevelsData.Level);

            _initialized = true;
            DontDestroyOnLoad(this);
        }

        public void OnLoad()
        {
            FindObjectOfType<BaseCompositionRoot>().Construct(_levelLoader, _analytics, _timeSpentTimer,
                _progressService, _saveLoadService);
        }

        private void Update()
        {
            if (!_initialized)
                return;

            _playTimeTimer.Tick(Time.unscaledDeltaTime);
            _timeSpentTimer.Tick(Time.unscaledDeltaTime);
            _userPlaytime.Update();
            _analyticsPlayTimeLogger.Update();
        }

        private void OnApplicationFocus(bool focus)
        {
            if (focus)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Source.Analytics;
using Source.Map;
using Source.SaveLoad;

namespace Source.LevelLoaderService
{
    internal class LevelLoadEventSender : ILevelLoader
    {
        private readonly ILevelLoader _levelLoader;
        private readonly IAnalyticsService _analyticsService;
        private readonly GameData _gameData;
        private readonly LevelTimer _levelTimer;

        public LevelLoadEventSender(GameData gameData, IAnalyticsService analyticsService, LevelTimer levelTimer,
            ILevelLoader levelLoader)
        {
            _levelTimer = levelTimer ?? throw new ArgumentNullException();
            _gameData = gameData ?? throw new ArgumentNullException();
            _analyticsService = analyticsService ?? throw new ArgumentNullException();
            _levelLoader = levelLoader ?? throw new ArgumentNullException();
        }

        public void LoadLevel(int levelNumber) =>
            LoadLevel(levelNumber, null);

        public void LoadMenu() =>
            LoadMenu(null);

        public void LoadMenu(Action action)
        {
            Dictionary<string, object> properties = GetProperties(_gameData, _levelTimer);

            _analyticsService.LogEvent("open_global_map", properties);
            _levelLoader.LoadMenu(action);
        }

        public void LoadLevel(int levelNumber, Action action)
        {
            Dictionary<string, object> properties = GetProperties(_gameData, _levelTimer, levelNumber);

            _analyticsService.LogEvent("enter_level", properties);
            _levelLoader.LoadLevel(levelNumber, action);
        }

        public static Dictionary<string, object> GetProperties(GameData gameData, LevelTimer levelTimer,
            int levelNumber = -1)
        {
            if (levelNumber == -1)
                levelNumber = gameData.LevelsData.Level;

            Dictionary<string, object> properties = new()
            {
                {"level", levelNumber},
                {"replay", gameData.LevelsData.Levels[levelNumber].Completed},
                {"minutes", levelTimer.ElapsedTime.Minutes},
                {"seconds", levelTimer.ElapsedTime.Seconds},
                {"complete_stations", gameData.LevelsData.Levels.Count(data => data.Completed)}
            };

            return properties;
        }
    }
}

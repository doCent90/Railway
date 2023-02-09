using System;
using System.Collections.Generic;
using Source.Analytics;
using Source.SaveLoad;

namespace Source.Map
{
    public class SendEventLevelFinish : ILevelFinish
    {
        private readonly ILevelFinish _levelFinishImplementation;
        private readonly IAnalyticsService _analyticsService;
        private readonly GameData _gameData;
        private readonly LevelTimer _levelTimer;

        public SendEventLevelFinish(IAnalyticsService analyticsService, GameData gameData,
            LevelTimer levelTimer, ILevelFinish levelFinish)
        {
            _levelTimer = levelTimer ?? throw new ArgumentNullException();
            _analyticsService = analyticsService ?? throw new ArgumentNullException();
            _gameData = gameData ?? throw new ArgumentNullException();
            _levelFinishImplementation = levelFinish ?? throw new ArgumentNullException();
        }

        public void Finish()
        {
            Dictionary<string, object> properties = new()
            {
                {"level", _gameData.LevelsData.Level},
                {"minutes", _levelTimer.ElapsedTime.Minutes},
                {"seconds", _levelTimer.ElapsedTime.Seconds}
            };

            _analyticsService.LogEvent("complete_building_level", properties);
            _levelFinishImplementation.Finish();
        }
    }
}

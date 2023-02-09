using System.Collections.Generic;
using FreeplaySDK;

namespace Source.Analytics
{
    class FreeplayAnalyticsService : IAnalyticsService
    {
        public void LogGameStart(int sessionNumber)
        {
        }

        public void LogLevelStart(int level)
        {
            Freeplay.NotifyLevelStart(level);
        }

        public void LogLevelRestart(int levelDataLevelNumber)
        {
        }

        public void LogLevelFail(int levelDataLevelNumber, float time, string reason = "")
        {
        }

        public void LogLevelWin(int levelDataLevelNumber, float timerPassedTime)
        {
            Freeplay.NotifyLevelCompleted(levelDataLevelNumber, true);
        }

        public void LogTime(int timeTotalMinutes)
        {
        }

        public void LogEvent(string name, Dictionary<string, object> properties)
        {
        }
    }
}

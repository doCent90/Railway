using System.Collections.Generic;

namespace Source.Analytics
{
    internal class NullAnalytics : IAnalyticsService
    {
        public void LogGameStart(int sessionNumber)
        {
        }

        public void LogLevelStart(int level)
        {
        }

        public void LogLevelRestart(int levelDataLevelNumber)
        {
        }

        public void LogLevelFail(int levelDataLevelNumber, float time, string reason = "")
        {
        }

        public void LogLevelWin(int levelDataLevelNumber, float timerPassedTime)
        {
        }

        public void LogTime(int timeTotalMinutes)
        {
        }

        public void LogEvent(string name, Dictionary<string, object> properties)
        {
        }
    }
}

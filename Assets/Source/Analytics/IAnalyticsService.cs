using System.Collections.Generic;

namespace Source.Analytics
{
    public interface IAnalyticsService
    {
        void LogGameStart(int sessionNumber);
        void LogLevelStart(int level);
        void LogLevelRestart(int levelDataLevelNumber);
        void LogLevelFail(int levelDataLevelNumber, float time, string reason = "");
        void LogLevelWin(int levelDataLevelNumber, float timerPassedTime);
        void LogTime(int timeTotalMinutes);
        void LogEvent(string name, Dictionary<string, object> properties);
    }
}

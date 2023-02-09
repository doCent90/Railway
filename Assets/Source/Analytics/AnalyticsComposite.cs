using System.Collections.Generic;

namespace Source.Analytics
{
    internal class AnalyticsComposite : IAnalyticsService
    {
        private readonly IAnalyticsService[] _analyticsServices;

        public AnalyticsComposite(params IAnalyticsService[] analyticsServices)
        {
            _analyticsServices = analyticsServices;
        }

        public void LogGameStart(int sessionNumber)
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogGameStart(sessionNumber);
        }

        public void LogLevelStart(int level)
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogLevelStart(level);
        }

        public void LogLevelRestart(int levelDataLevelNumber)
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogLevelRestart(levelDataLevelNumber);
        }

        public void LogLevelFail(int levelDataLevelNumber, float time, string reason = "")
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogLevelFail(levelDataLevelNumber, time, reason);
        }

        public void LogLevelWin(int levelDataLevelNumber, float timerPassedTime)
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogLevelWin(levelDataLevelNumber, timerPassedTime);
        }

        public void LogTime(int timeTotalMinutes)
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogTime(timeTotalMinutes);
        }

        public void LogEvent(string name, Dictionary<string, object> properties)
        {
            foreach (IAnalyticsService analyticsService in _analyticsServices)
                analyticsService.LogEvent(name, properties);
        }
    }
}

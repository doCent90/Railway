using System;

namespace Source.Analytics
{
    public class AnalyticsPlayTimeLogger
    {
        private const int TimeAfterChangeIntervalInMinutes = 10;
        private const int FirstIntervalInMinutes = 1;
        private const int SecondIntervalInMinutes = 5;
        
        private readonly IUserPlaytime _userPlaytime;
        private readonly IAnalyticsService _analytics;
        
        private double _endMinutes;

        private TimeSpan _time => _userPlaytime.AllPlayTime;

        public AnalyticsPlayTimeLogger(IUserPlaytime userPlaytime, IAnalyticsService analytics)
        {
            _userPlaytime = userPlaytime;
            _analytics = analytics;
            UpdateEndMinutes();
        }

        public void Update()
        {
            if (_time.TotalMinutes >= _endMinutes)
                _analytics.LogTime((int)_time.TotalMinutes);

            UpdateEndMinutes();
        }

        private void UpdateEndMinutes()
        {
            var interval = _time.TotalMinutes >= TimeAfterChangeIntervalInMinutes
                ? SecondIntervalInMinutes
                : FirstIntervalInMinutes;
            
            _endMinutes = interval * ((int) _time.TotalMinutes / interval) + interval;
        }
    }
}

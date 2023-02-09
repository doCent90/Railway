using System;
using Source.Analytics.Timer;
using Source.SaveLoad;
using UnityEngine;

namespace Source.Analytics
{
    public class UserPlaytime : IUserPlaytime
    {
        private const float DelayBetweenAddIntervalInSeconds = 1;

        private readonly ITimerService _timer;
        private readonly AnalyticsData _analyticsData;

        private TimeSpan _time;
        
        public TimeSpan AllPlayTime => _time;
        
        public UserPlaytime(ITimerService timer, AnalyticsData analyticsData)
        {
            _analyticsData = analyticsData;
            _timer = timer;
            _time = TimeSpan.Parse(analyticsData.AllPlayTime);
        }

        public void Update()
        {
            if (_timer.ElapsedTime < DelayBetweenAddIntervalInSeconds)
                return;

            _time = _time.Add(TimeSpan.FromSeconds(DelayBetweenAddIntervalInSeconds));
            _timer.Reset();
            _analyticsData.AllPlayTime = _time.ToString();
        }
    }
}
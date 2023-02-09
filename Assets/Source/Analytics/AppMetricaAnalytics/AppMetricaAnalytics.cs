using System;
using System.Collections.Generic;
using Source.Analytics.SoftCurrency;

namespace Source.Analytics.AppMetricaAnalytics
{
    public class AppMetricaAnalytics : IAnalyticsService, ISoftCurrencyAnalytics
    {
        private const int SecondsInMinute = 60;

        private readonly IUserPlaytime _userPlaytime;
        private readonly IYandexAppMetrica _appMetrica;

        public AppMetricaAnalytics(IYandexAppMetrica appMetrica, IUserPlaytime userPlaytime)
        {
            _userPlaytime = userPlaytime;
            _appMetrica = appMetrica;
        }

        public void LogGameStart(int sessionNumber)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>()
            {
                {EventProperties.Count, sessionNumber}
            };

            FireEventWithTotalPlaytime(EventNames.GameStart, properties);
        }

        public void LogLevelStart(int number)
        {
            var properties = new Dictionary<string, object>();
            AddLevelData(properties, number);
            FireEventWithTotalPlaytime(EventNames.LevelStart, properties);
        }

        public void LogLevelRestart(int number)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            AddLevelData(properties, number);
            FireEventWithTotalPlaytime(EventNames.LevelRestart, properties);
        }

        public void LogLevelFail(int number, float time, string reason = "")
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            AddLevelData(properties, number);
            AddTimeSpent(properties, time);
            FireEventWithTotalPlaytime(EventNames.LevelLost, properties);
        }

        public void LogLevelWin(int number, float time)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>();
            AddLevelData(properties, number);
            AddTimeSpent(properties, time);
            FireEventWithTotalPlaytime(EventNames.LevelComplete, properties);
        }

        public void LogTime(int elapsedMinutes)
        {
            var properties = new Dictionary<string, object>()
            {
                {EventProperties.ElapsedMinutes, elapsedMinutes}
            };

            FireEventWithTotalPlaytime(EventNames.PlayTime, properties);
        }

        public void LogEvent(string name, Dictionary<string, object> properties) =>
            FireEventWithTotalPlaytime(name, properties);

        private void AddTimeSpent(Dictionary<string, object> properties, float time) =>
            properties.Add(EventProperties.TimeSpent, time);

        private void AddLevelData(Dictionary<string, object> properties, int number)
        {
            properties.Add(EventProperties.Number, number);
        }

        private void FireEventWithTotalPlaytime(string eventName, Dictionary<string, object> eventProps)
        {
            if (eventProps == null)
                throw new ArgumentNullException(nameof(eventProps));

            eventProps.Add(EventProperties.TotalPlaytimeMIN, _userPlaytime.AllPlayTime.Minutes);
            eventProps.Add(EventProperties.TotalPlaytimeSec,
                _userPlaytime.AllPlayTime.Minutes * SecondsInMinute + _userPlaytime.AllPlayTime.Seconds);

            _appMetrica.ReportEvent(eventName, eventProps);
        }

        public void LogSoftSpent(string purchaseType, int amount)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>()
            {
                {SoftCurrencyConstants.PurchaseType, purchaseType},
                {SoftCurrencyConstants.Spent, amount}
            };

            FireEventWithTotalPlaytime(SoftCurrencyConstants.SoftSpent, properties);
        }

        public void LogSoftSpent(string purchaseType, string productType, int amount)
        {
            Dictionary<string, object> properties = new Dictionary<string, object>()
            {
                {SoftCurrencyConstants.PurchaseType, purchaseType},
                {SoftCurrencyConstants.ProductType, productType},
                {SoftCurrencyConstants.Spent, amount}
            };

            FireEventWithTotalPlaytime(SoftCurrencyConstants.SoftSpent, properties);
        }
    }
}

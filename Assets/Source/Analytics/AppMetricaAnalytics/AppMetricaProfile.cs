using System;
using System.Globalization;
using Source.SaveLoad;

namespace Source.Analytics.AppMetricaAnalytics
{
    public class AppMetricaProfile
    {
        private const string SessionCount = "session_count";
        private const string DaysInGame = "days_in_game";
        private const string DateSaveFormat = "dd/MM/yy";
        private const string DateFormat = "dd.MM.yy";
        
        private readonly AnalyticsData _analyticsData;
        private readonly IYandexAppMetrica _appMetrica;

        private YandexAppMetricaUserProfile _userProfile;

        public AppMetricaProfile(AnalyticsData analyticsData, IYandexAppMetrica appMetrica)
        {
            _analyticsData = analyticsData;
            _appMetrica = appMetrica;
        }

        public void InitAppMetricaProfile()
        {
            _userProfile = new YandexAppMetricaUserProfile();
            
            int sessionNumber = _analyticsData.SessionId += 1;
            

            if (sessionNumber == 1)
                SetRegDay();

            SetDaysInGame();
            SetSessionCount(sessionNumber);

            _appMetrica.ReportUserProfile(_userProfile);
        }

        private void SetSessionCount(int sessionNumber)
        {
            _userProfile.Apply(YandexAppMetricaAttribute.CustomCounter(SessionCount).WithDelta(sessionNumber));
        }

        private void SetDaysInGame()
        {
            DateTime now = DateTime.Now;
            DateTime regDay = DateTime.ParseExact(_analyticsData.RegDay, DateFormat, null);
            var days = now - regDay;
            _userProfile.Apply(YandexAppMetricaAttribute.CustomCounter(DaysInGame).WithDelta(days.Days));
        }

        private void SetRegDay()
        {
            DateTime now = DateTime.Now;
            string date = now.ToString(DateFormat);
            _analyticsData.RegDay = date;
            _userProfile.Apply(YandexAppMetricaAttribute.CustomString(DateSaveFormat).WithValue(now.ToString(CultureInfo.InvariantCulture)));
        }
    }
}
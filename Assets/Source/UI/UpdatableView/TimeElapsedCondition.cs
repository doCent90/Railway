using System;
using Source.Map;

namespace Source.UI.UpdatableView
{
    public class TimeElapsedCondition : IUpdateCondition
    {
        private readonly IUpdateCondition _updateCondition;
        private readonly LevelTimer _levelTimer;
        private readonly float _seconds;

        public TimeElapsedCondition(LevelTimer levelTimer, int seconds,
            OfflineIncomeCollectedCondition condition)
        {
            _seconds = seconds;
            _levelTimer = levelTimer ?? throw new ArgumentNullException();
            _updateCondition = condition ?? throw new ArgumentNullException();
        }

        public bool Updated => _levelTimer.ElapsedTime.TotalSeconds > _seconds && _updateCondition.Updated;
    }
}

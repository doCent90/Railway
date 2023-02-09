using System;
using Source.Characters.Upgrades;
using Source.SaveLoad;
using UnityEngine;

namespace Source.GlobalMap
{
    public class OfflineIncomeHelper
    {
        private readonly CompleteLevelData _completeLevelData;
        private readonly UpgradeValue<float> _costUpgradeValue;
        private readonly UpgradeValue<int> _capacity;
        private readonly UpgradeValue<float> _incomeUpgradeValue;

        public OfflineIncomeHelper(CompleteLevelData completeLevelData, UpgradeValue<float> costUpgradeValue,
            UpgradeValue<int> capacity, UpgradeValue<float> incomeUpgradeValue)
        {
            _incomeUpgradeValue = incomeUpgradeValue;
            _costUpgradeValue = costUpgradeValue ?? throw new ArgumentNullException();
            _completeLevelData = completeLevelData ?? throw new ArgumentNullException();
            _capacity = capacity ?? throw new ArgumentNullException();
        }

        public float Collected => GetNormalizedProgress() * _costUpgradeValue.Value * _capacity.Value;
        public bool FullProgress => Math.Abs(GetNormalizedProgress() - 1f) < 0.0001f;

        public float GetNormalizedProgress() =>
            Mathf.InverseLerp(0f, (float)GetTimeToNextIncomeClaim().TotalSeconds, (float)GetElapsedTime().TotalSeconds);

        public void UpdateLastClaimForNormalizedProgress(float normalizedDistance)
        {
            TimeSpan newElapsed = TimeSpan.FromSeconds(GetTimeToNextIncomeClaim().TotalSeconds * normalizedDistance);
            _completeLevelData.LastIncomeClaim = DateTime.Now - newElapsed;
        }

        public TimeSpan GetRemainingTime() =>
            GetTimeToNextIncomeClaim() - GetElapsedTime();

        public void Claim()
        {
            _completeLevelData.LastIncomeClaim = DateTime.Now;
            _completeLevelData.Claims++;
        }

        public TimeSpan GetElapsedTime() =>
            DateTime.Now - _completeLevelData.LastIncomeClaim;

        public float GetMaxIncomeClaimCapacity() => _capacity.Value * _costUpgradeValue.Value;

        private TimeSpan GetTimeToNextIncomeClaim()
        {
            float time = _capacity.Value * _costUpgradeValue.Value / _incomeUpgradeValue.Value;

            return TimeSpan.FromSeconds(time);
        }
    }
}

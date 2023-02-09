using System;
using Source.Characters.Upgrades;
using UnityEngine;

namespace Source.GlobalMap
{
    public class AccumulatedIncomeProvider
    {
        private readonly UpgradeValue<int> _capacity;
        private readonly UpgradeValue<float> _cost;

        public int Capacity => _capacity.Value;

        public AccumulatedIncomeProvider(UpgradeValue<float> cost, UpgradeValue<int> capacity)
        {
            _cost = cost ?? throw new ArgumentNullException();
            _capacity = capacity ?? throw new ArgumentNullException();
        }

        public float GetForProgress(float progress)
        {
            return (int)Mathf.Lerp(0, _cost.Value * _capacity.Value, progress);
        }
    }
}

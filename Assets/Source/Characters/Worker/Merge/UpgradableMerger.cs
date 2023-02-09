using System;
using Source.Characters.Upgrades;
using Source.Characters.Worker.Merge.GenericMerge;

namespace Source.Characters.Worker.Merge
{
    public class UpgradableMerger<TMergeableConfig, TMergeable> where TMergeableConfig : class
    {
        private readonly UpgradeValue<int> _upgradeValue;
        private readonly UpgradeLevel _mergeUpgradeLevel;
        private readonly Merger<TMergeableConfig, TMergeable> _merger;

        private int _spawnedAmount;
        private int _merges;

        public UpgradableMerger(UpgradeValue<int> upgradeValue, UpgradeLevel mergeUpgradeLevel,
            Merger<TMergeableConfig, TMergeable> merger)
        {
            _mergeUpgradeLevel = mergeUpgradeLevel ?? throw new ArgumentException();
            _upgradeValue = upgradeValue ?? throw new ArgumentException();
            _merger = merger ?? throw new ArgumentException();
        }

        public void Start()
        {
            _spawnedAmount = _upgradeValue.Value;
            _merges = _mergeUpgradeLevel.Level;
            _merger.SpawnStart(_spawnedAmount, _mergeUpgradeLevel.Level);
        }

        public void Update()
        {
            if (_spawnedAmount == _upgradeValue.Value - 1)
                SpawnNew();

            if (_merger.CanMerge() && _merges < _mergeUpgradeLevel.Level)
            {
                _merges++;
                _merger.Merge();
            }
        }

        private void SpawnNew()
        {
            for (int i = 0; i < _upgradeValue.Value - _spawnedAmount; i++)
            {
                _merger.SpawnNew();
                _spawnedAmount++;
            }
        }
    }
}

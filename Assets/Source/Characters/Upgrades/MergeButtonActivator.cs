using System;
using Source.Characters.Worker.Merge.GenericMerge;
using UnityEngine;

namespace Source.Characters.Upgrades
{
    public class MergeButtonActivator : MonoBehaviour
    {
        [SerializeField] private UpgradePurchase _upgradePurchase;

        private UpgradeLevel _workersUpgradeLevel;
        private UpgradeLevel _mergeUpgradeLevel;
        private int _workerVariantsCount;

        public void Construct(UpgradeLevel workersUpgradeLevel, UpgradeLevel mergeUpgradeLevel, int workerVariantsCount)
        {
            _workerVariantsCount = workerVariantsCount;
            _mergeUpgradeLevel = mergeUpgradeLevel ?? throw new ArgumentException();
            _workersUpgradeLevel = workersUpgradeLevel ?? throw new ArgumentException();
        }

        private void Update()
        {
            bool active = CountPossibleMergeCount(_workersUpgradeLevel.Level) > _mergeUpgradeLevel.Level;
            _upgradePurchase.gameObject.SetActive(active);
        }

        private int CountPossibleMergeCount(int level)
        {
            var count = 0;

            for (int i = 1; i < _workerVariantsCount + 1; i++)
                count += level / (int) Mathf.Pow(3, i);

            return count;
        }
    }
}

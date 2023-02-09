using System;

namespace Source.Characters.Upgrades
{
    internal class ResourceCostFactory
    {
        private readonly UpgradeLevel _upgradeLevel;

        public ResourceCostFactory(UpgradeLevel upgradeLevel)
        {
            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException();
        }

        public UpgradeValue<float> Create() =>
            new(_upgradeLevel, new OfflineIncomeProgression(10, 2f));
    }
}

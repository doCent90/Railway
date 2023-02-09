using System;

namespace Source.Characters.Upgrades
{
    internal class IncomeFactory
    {
        private readonly UpgradeLevel _upgradeLevel;

        public IncomeFactory(UpgradeLevel upgradeLevel)
        {
            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException();
        }

        public UpgradeValue<float> Create() =>
            new(_upgradeLevel, new OfflineIncomeProgression(1, 1f));
    }
}

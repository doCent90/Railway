namespace Source.Characters.Upgrades
{
    internal class ResourceCapacityValueFactory
    {
        private readonly UpgradeLevel _upgradeLevel;

        public ResourceCapacityValueFactory(UpgradeLevel upgradeLevel)
        {
            _upgradeLevel = upgradeLevel;
        }

        public UpgradeValue<int> Create() =>
            new(_upgradeLevel, new LevelIntProgression(0, 8));
    }
}

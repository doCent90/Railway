namespace Source.Characters.Upgrades
{
    internal class StraightIntUpgradeValue : UpgradeValue<int>
    {
        public StraightIntUpgradeValue(UpgradeLevel upgradeLevel) : base(upgradeLevel, new LevelIntProgression(0, 1))
        {
            
        }
    }
}
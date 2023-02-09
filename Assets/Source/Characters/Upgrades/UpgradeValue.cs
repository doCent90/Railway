using System;

namespace Source.Characters.Upgrades
{
    public class UpgradeValue<TType>
    {
        private readonly LevelProgression<TType> _levelFloatProgression;
        private readonly UpgradeLevel _upgradeLevel;

        public UpgradeValue(UpgradeLevel upgradeLevel, LevelProgression<TType> levelFloatProgression)
        {
            _levelFloatProgression = levelFloatProgression ?? throw new ArgumentException();
            _upgradeLevel = upgradeLevel ?? throw  new ArgumentException();
        }

        public TType Value => _levelFloatProgression.ForLevel(_upgradeLevel.Level);
    }
}

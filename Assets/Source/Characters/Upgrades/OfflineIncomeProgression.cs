namespace Source.Characters.Upgrades
{
    internal class OfflineIncomeProgression : LevelProgression<float>
    {
        private readonly float _addPerLevel;
        private readonly int _startValue;

        public OfflineIncomeProgression(int startValue, float addPerLevel)
        {
            _startValue = startValue;
            _addPerLevel = addPerLevel;
        }

        public override float ForLevel(int currentLevel)
        {
            return _startValue + currentLevel * _addPerLevel;
        }
    }
}

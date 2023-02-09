using System;
using UnityEngine;

namespace Source.Characters.Upgrades
{
    [Serializable]
    public class LevelFloatProgression : LevelProgression<float>
    {
        [SerializeField] private float _startValue;
        [SerializeField] private float _addPerLevel;
        [SerializeField] private int _maxLevel;
        [SerializeField] private int _levelToMultiply;
        [SerializeField] private float _addMultiplierAfterLevel;

        public override float ForLevel(int currentLevel)
        {
            if (_maxLevel != 0)
                currentLevel = Mathf.Clamp(currentLevel - 1, 0, _maxLevel);

            float addMultiplier = Mathf.Clamp(currentLevel - _levelToMultiply, 0, float.MaxValue) *
                                  _addMultiplierAfterLevel;

            if (_levelToMultiply == 0)
                addMultiplier = 0;

            return _startValue + _addPerLevel * (currentLevel - 1 + addMultiplier);
        }
    }
}
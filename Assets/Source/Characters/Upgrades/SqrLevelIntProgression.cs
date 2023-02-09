using System;
using UnityEngine;

namespace Source.Characters.Upgrades
{
    [Serializable]
    internal class SqrLevelIntProgression : LevelProgression<int>
    {
        [SerializeField] private float _addToLevel;
        [SerializeField] private float _levelMultiplier;
        [SerializeField] private float _multiplicityDecrease;

        public SqrLevelIntProgression(float perLevel)
        {
            _addToLevel = perLevel;
        }

        public override int ForLevel(int currentLevel)
        {
            float decrease = _multiplicityDecrease != 0 ? currentLevel / _multiplicityDecrease : 0;
            return (int) (10f * Mathf.Pow((currentLevel + _addToLevel - decrease) * _levelMultiplier, 1.5f));
        }
    }
}
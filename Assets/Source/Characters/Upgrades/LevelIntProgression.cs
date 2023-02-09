using System;
using UnityEngine;

namespace Source.Characters.Upgrades
{
    [Serializable]
    public class LevelIntProgression : LevelProgression<int>
    {
        [SerializeField] private int _startValue;
        [SerializeField] private int _addPerLevel;

        public LevelIntProgression(int start, int perLevel)
        {
            if (start < 0 || perLevel < 0)
                throw new ArgumentException();
            
            _startValue = start;
            _addPerLevel = perLevel;
        }

        public override int ForLevel(int currentLevel) => 
            _startValue + _addPerLevel * currentLevel;
    }
}
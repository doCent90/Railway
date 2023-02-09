using System;
using UnityEngine;

namespace Source.Characters.Upgrades
{
    [Serializable]
    public class UpgradeLevel
    {
        [SerializeField]
        private int _level;

        public UpgradeLevel(int level)
        {
            _level = level;
        }

        public int Level => _level;

        public void AddLevel()
        {
            _level++;
        }
    }
}
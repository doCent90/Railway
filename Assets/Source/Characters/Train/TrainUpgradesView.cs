using System.Collections.Generic;
using Source.Characters.Upgrades;
using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainUpgradesView : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _upgrades;

        private UpgradeLevel _trainUpgrade;
        private int _upgrade = 1;

        public void Construct(UpgradeLevel trainUpgrade) =>
            _trainUpgrade = trainUpgrade;

        private void Update()
        {
            if (_trainUpgrade.Level != _upgrade)
                Upgrade();
        }

        private void Upgrade()
        {
            _upgrade++;

            for (var i = 0; i < _upgrade; i++)
                _upgrades[i].gameObject.SetActive(i == _upgrade - 1);
        }
    }
}
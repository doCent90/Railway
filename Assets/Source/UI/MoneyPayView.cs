using System;
using Source.Money;
using UnityEngine;

namespace Source.UI
{
    public class MoneyPayView : MonoBehaviour, IEnvironmentPayer
    {
        [SerializeField] private BonusText _bonusTextTemplate;
        [SerializeField] private Vector3 _offset;

        private MoneyBalance _moneyBalance;

        public void Construct(MoneyBalance moneyBalance) => 
            _moneyBalance = moneyBalance ?? throw new ArgumentException();

        public void Pay(int amount, Vector3 transformPosition)
        {
            _moneyBalance.Add(amount);
            SpawnBonusParticles(amount, transformPosition);
        }

        private void SpawnBonusParticles(int amount, Vector3 position)
        {
            BonusText bonusText = Instantiate(_bonusTextTemplate);
            bonusText.transform.position = position + _offset;
            bonusText.SetAmount(amount);
        }
    }
}

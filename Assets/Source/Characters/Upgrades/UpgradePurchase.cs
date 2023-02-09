using System;
using Source.Money;
using Source.UI;
using UnityEngine;

namespace Source.Characters.Upgrades
{
    public class UpgradePurchase : MonoBehaviour
    {
        [SerializeField] private SqrLevelIntProgression _levelCostProgression;
        [SerializeField] private PurchaseView _button;
        [SerializeField] private int _maxLevel;

        private UpgradeLevel _upgradeLevel;
        private MoneyBalance _moneyBalance;
        private ICanBuyCondition _canBuyCondition;

        private int _price => _levelCostProgression.ForLevel(_level);
        private int _level => _upgradeLevel.Level;

        public void Construct(MoneyBalance moneyBalance, UpgradeLevel upgradeLevel, ICanBuyCondition canBuyCondition)
        {
            _canBuyCondition = canBuyCondition ?? throw new ArgumentNullException();
            _moneyBalance = moneyBalance ?? throw new ArgumentNullException();
            _upgradeLevel = upgradeLevel ?? throw new ArgumentNullException();
        }

        private void OnEnable() =>
            _button.Clicked += TryBuy;

        private void Update() =>
            UpdateView();

        private void Start() =>
            UpdateView();

        private void OnDisable() =>
            _button.Clicked -= TryBuy;

        private void TryBuy()
        {
            if (CanBuy())
                Buy();
        }

        private bool CanBuy() =>
            _moneyBalance.Value >= _price && _canBuyCondition.CanBuy() && AchievedMaxLevel() == false;

        private void Buy()
        {
            _moneyBalance.Spend(_price);
            _upgradeLevel.AddLevel();
            UpdateView();
        }

        private void UpdateView()
        {
            _button.Display(CanBuy(), _level, _price);

            if (AchievedMaxLevel())
                _button.DisplayMaxLevel();
        }

        private bool AchievedMaxLevel() => _maxLevel != 0 && _maxLevel == _upgradeLevel.Level;
    }
}

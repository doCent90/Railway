using System;
using Source.Money;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;

        private MoneyBalance _moneyBalance;

        public void Construct(MoneyBalance moneyBalance) =>
            _moneyBalance = moneyBalance ?? throw new ArgumentException();

        private void Start() =>
            Display();

        private void Update() =>
            Display();

        private void Display() =>
            _text.text = _moneyBalance.Value.ToString();
    }
}
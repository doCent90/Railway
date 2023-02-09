using System;
using Source.Characters.Train.Payment;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class SellButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Button _button;

        private CollectedResourcesSell _sell;

        public void Construct(CollectedResourcesSell counter)
        {
            _sell = counter ?? throw new ArgumentNullException();
        }

        private void OnEnable() =>
            _button.onClick.AddListener(Sell);

        private void Update()
        {
            float totalCost = _sell.GetTotalCost();
            float maxCapacity = _sell.GetMaxCostCapacity();
            _amountText.text = $"{totalCost}/{maxCapacity}";
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(Sell);

        private void Sell()
        {
            float cost = _sell.GetTotalCost();

            if (cost == 0)
                return;

            _sell.Sell();
        }
    }
}

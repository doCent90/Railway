using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class PurchaseView : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private Sprite _hasMoneyColor;
        [SerializeField] private Sprite _noHasMoneyColor;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private string _currentLevelPattern;

        public event Action Clicked;

        private void OnEnable() =>
            _button.onClick.AddListener(InvokeClicked);

        private void OnDisable() =>
            _button.onClick.RemoveListener(InvokeClicked);

        private void InvokeClicked() =>
            Clicked?.Invoke();

        public void Display(bool canBuy, int currentLevel, int currentPrice)
        {
            _button.image.sprite = canBuy ? _hasMoneyColor : _noHasMoneyColor;
            _priceText.text = currentPrice.ToString();
            _levelText.text = string.Format(_currentLevelPattern, currentLevel);
        }

        public void DisplayMaxLevel()
        {
            _priceText.text = "Max";
        }
    }
}

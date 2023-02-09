using System;
using System.Collections;
using DG.Tweening;
using Source.Map;
using Source.Money;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    internal class LevelCompleteScreen : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _reward;

        private IEnvironmentPayer _environmentPayer;
        private ILevelFinish _buildingLevelFinish;
        private int _amount;
        private float _payWaitDelay = 2f;
        private IHidableView _hidableView;

        public void Construct(ILevelFinish buildingLevelFinish, IEnvironmentPayer environmentPayer, int amount,
            IHidableView hidableView)
        {
            _hidableView = hidableView;
            _amount = amount;
            _environmentPayer = environmentPayer;
            _buildingLevelFinish = buildingLevelFinish;
            _reward.text = amount.ToString();
            _button.onClick.AddListener(PressButton);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(PressButton);
        }

        private void PressButton()
        {
            StartCoroutine(Pay());
        }

        private IEnumerator Pay()
        {
            _environmentPayer.Pay(_amount, _button.transform.position);
            yield return new WaitForSeconds(1f);
            _hidableView.Show();
            transform.DOScale(Vector3.zero, 0.2f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
            yield return new WaitForSeconds(1.5f);
            _buildingLevelFinish.Finish();
        }

        public void Show()
        {
            gameObject.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            transform.localScale = Vector3.zero;
            sequence.Append(transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.Linear));
            sequence.Append(transform.DOPunchScale(Vector3.one * 0.2f, 0.2f));
        }
    }
}

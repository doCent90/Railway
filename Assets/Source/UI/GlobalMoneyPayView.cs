using System.Collections;
using DG.Tweening;
using Source.Money;
using UnityEngine;

namespace Source.UI
{
    public class GlobalMoneyPayView : MonoBehaviour, IEnvironmentPayer
    {
        private const float DurationScaleStart = 0.2f;
        private const float DurationScaleEnd = 0.1f;
        private const float DurationMove = 1f;
        private const float PunchFactor = 1.3f;
        private const float WaitCreate = 0.05f;
        private const int Count = 5;

        [SerializeField] private Canvas _bonusTextTemplate;
        [SerializeField] private float _offset = 0.2f;

        private Transform _moneyView;
        private MoneyBalance _moneyBalance;
        private Tween _icon;

        public void Construct(MoneyBalance moneyBalance, Transform moneyView)
        {
            _moneyBalance = moneyBalance;
            _moneyView = moneyView;
        }

        public void Pay(int amount, Vector3 position) => CreateParticles(amount, position, Count);

        private void CreateParticles(int amount, Vector3 position, int count)
        {
            StartCoroutine(Creating(amount, position));

            IEnumerator Creating(int amount, Vector3 position)
            {
                var wait = new WaitForSecondsRealtime(WaitCreate);

                for (int i = 0; i < count; i++)
                {
                    yield return wait;
                    Create(_bonusTextTemplate, position, i + 1, amount);
                }
            }
        }

        private void Create(Canvas bonusTextTemplate, Vector3 position, int count, int amount)
        {
            Canvas money = Instantiate(bonusTextTemplate, GetRandomPosition(position), Quaternion.identity, transform);
            money.transform.localScale = Vector3.zero;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(money.transform.DOScale(1.2f, DurationScaleStart));
            sequence.Append(money.transform.DOScale(1, DurationScaleEnd));
            sequence.Append(money.transform.DOMove(_moneyView.transform.position, DurationMove)
                .SetEase(Ease.InSine).SetDelay(DurationMove / 2));
            sequence.OnComplete(() =>
            {
                if (count == Count)
                    AddMoney(amount);

                AnimateIcon();
                Destroy(money.gameObject);
            });
        }

        private void AddMoney(int amount) => _moneyBalance.Add(amount);

        private void AnimateIcon()
        {
            if (_icon != null)
                _icon.Complete();

            _icon = _moneyView.transform.DOPunchScale(Vector3.one * PunchFactor, DurationScaleStart, 0, 1);
        }

        private Vector3 GetRandomPosition(Vector3 position)
        {
            float xOffset = Random.Range(-_offset, _offset);
            float yOffset = Random.Range(-_offset, _offset);

            Vector3 random = position + Vector3.right * xOffset + Vector3.up * yOffset;
            return random;
        }

        private void OnDestroy() => DOTween.CompleteAll();
    }
}

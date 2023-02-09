using DG.Tweening;
using UnityEngine;

namespace Source.UI
{
    public class TapClickView : MonoBehaviour
    {
        private const float Duration = 0.1f;

        [SerializeField] private RectTransform _point;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
                OnClicked(Input.mousePosition);
        }

        private void OnClicked(Vector3 pointPosition)
        {
            _point.position = pointPosition;

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_point.DOScale(Vector3.one * 3, Duration));
            sequence.Append(_point.DOScale(Vector3.zero, Duration));
        }
    }
}

using UnityEngine;

namespace Source.UI
{
    public class ButtonWorldToScreenPosition : MonoBehaviour
    {
        [SerializeField] private Transform _targetWorldPosition;

        private Camera _camera;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _camera = Camera.main;
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (_targetWorldPosition != null)
                _rectTransform.anchoredPosition = _camera.WorldToScreenPoint(_targetWorldPosition.position);
        }

        public void SetTargetPosition(Transform target)
        {
            _targetWorldPosition = target;
            Update();
        }
    }
}

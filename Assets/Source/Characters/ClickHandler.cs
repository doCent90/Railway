using UnityEngine;

namespace Source.Characters
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private float _clickIncreaseDuration = 0.3f;
        [SerializeField] private float _increasedTimeScale = 1.5f;

        private float _lastClick;
        private float _targetTimeScale;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _lastClick = Time.unscaledTime;
                _targetTimeScale = _increasedTimeScale;
            }

            if (_lastClick + _clickIncreaseDuration < Time.unscaledTime)
                _targetTimeScale = 1;

            Time.timeScale = Mathf.Lerp(Time.timeScale, _targetTimeScale, 0.5f);
        }
    }
}
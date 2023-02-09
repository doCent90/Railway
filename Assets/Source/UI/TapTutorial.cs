using TMPro;
using UnityEngine;

namespace Source.UI
{
    internal class TapTutorial : MonoBehaviour, ITutorial
    {
        private const float WaitTimeSec = 10f;

        [SerializeField] private TMP_Text _text;

        private float _timeWithoutClicks = WaitTimeSec;

        public bool Completed => false;

        private void Update()
        {
            _timeWithoutClicks += Time.unscaledDeltaTime;

            if (Input.GetMouseButtonDown(0))
                _timeWithoutClicks = 0;

            _text.gameObject.SetActive(_timeWithoutClicks >= WaitTimeSec);
        }

        public void StartTutorial()
        {
            enabled = true;
            _timeWithoutClicks = 0;
        }

        public void Stop()
        {
            enabled = false;
            _timeWithoutClicks = 0;
            _text.gameObject.SetActive(false);
        }

        public void Finish() => Stop();
    }
}

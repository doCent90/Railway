using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Source.Extensions;
using DG.Tweening;

namespace Source.UI
{
    public class BuiltLevelMapTutorial : MonoBehaviour
    {
        private const string TutorFirstDone = nameof(TutorFirstDone);
        private const string TutorSecondDone = nameof(TutorSecondDone);
        private const float PunchScaleFactor = -0.3f;
        private const float Duration = 1f;
        private const float Offset = 80f;
#if UNITY_EDITOR
        private const float WaitTimeFullStack = 5f;
#endif
#if !UNITY_EDITOR
        private const float WaitTimeFullStack = 50f;
#endif
        private const float WaitTimeSell = 5f;

        [SerializeField] private Button _map;
        [SerializeField] private Button _sell;
        [SerializeField] private Image _symbol;
        [SerializeField] private Image _finger;
        [SerializeField] private CanvasGroup _textGroupSell;
        [SerializeField] private CanvasGroup _textGroupMap;
        [SerializeField] private CanvasGroup _tapTutor;

        private bool _isClicked = false;

        private void Start()
        {
            _sell.onClick.AddListener(Cliked);

            if (PlayerPrefs.HasKey(TutorSecondDone))
                return;

            StartCoroutine(TutorialStarting());
        }

        private void OnDisable() => _sell.onClick.RemoveListener(Cliked);

        private void Cliked() => _isClicked = true;

        private IEnumerator TutorialStarting()
        {
            var waitSell = new WaitForSecondsRealtime(WaitTimeSell);
            var waitSellButtonClick = new WaitUntil(() => _isClicked);
            var waitFullStack = new WaitForSecondsRealtime(WaitTimeFullStack);
            var waitDisableTutor = new WaitForSecondsRealtime(WaitTimeFullStack * 2);

            if(PlayerPrefs.HasKey(TutorFirstDone) == false)
            {
                yield return waitSell;
                _textGroupSell.EnableFade();
                _tapTutor.DisableFade();
                SetFingetPosition(_sell.transform.position);
                yield return waitSellButtonClick;
                _textGroupSell.DisableFade();
                _finger.gameObject.SetActive(false);
                _tapTutor.EnableFade();
                PlayerPrefs.SetString(TutorFirstDone, TutorFirstDone);
            }

            yield return waitFullStack;
            _symbol.gameObject.SetActive(true);
            _textGroupMap.EnableFade();
            SetFingetPosition(_map.transform.position);
            _tapTutor.DisableFade();
            PlayerPrefs.SetString(TutorSecondDone, TutorSecondDone);
            yield return waitDisableTutor;
            _textGroupMap.DisableFade();
            _finger.gameObject.SetActive(false);
            _tapTutor.EnableFade();
        }

        private void SetFingetPosition(Vector3 position)
        {
            _finger.gameObject.SetActive(true);
            _finger.transform.position = new Vector3(position.x + Offset, position.y - Offset, position.z);
            Animate();
        }

        private void Animate()
            => _finger.transform.DOPunchScale(Vector3.one * PunchScaleFactor, Duration, 0, 0).SetLoops(-1);
    }
}

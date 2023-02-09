using System.Collections;
using Source.Extensions;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Source.UI
{
    public class GlobalMapTutorial : MonoBehaviour
    {
        private const string SecondBuildLevelClick = nameof(SecondBuildLevelClick);
        private const string FirstBuiltLevelClick = nameof(FirstBuiltLevelClick);
        private const string GetRewardlClick = nameof(GetRewardlClick);
        private const string ScrollMap = nameof(ScrollMap);
        private const float OunchScaleFactor = -0.3f;
        private const float Duration = 1f;

        [SerializeField] private Transform _camera;
        [SerializeField] private Transform _finger;
        [SerializeField] private Button _secondBuildLevelButton;
        [SerializeField] private Button _getRewardTrain1Button;
        [SerializeField] private Button _getRewardTrain2Button2;
        [SerializeField] private Button _openTrainMenuButton;
        [SerializeField] private Button _openTrain2MenuButton;
        [SerializeField] private Button _goBuiltLevel1Button;
        [SerializeField] private Button _bildLevel3Button;
        [SerializeField] private Button _closeMenuButton;
        [SerializeField] private CanvasGroup _firstText;
        [SerializeField] private CanvasGroup _secondText;
        [SerializeField] private CanvasGroup _secondPart2Text;
        [SerializeField] private CanvasGroup _thirdText;
        [SerializeField] private CanvasGroup _fourthText;
        [SerializeField] private GlobalMapScroller _globalMapScroller;

        private Transform _position;
        private Coroutine _coroutine;
        private bool _buttonCliked = false;

        private void Awake()
        {
            if (PlayerPrefs.HasKey(ScrollMap) == false)
                StartTutorial();

            _openTrainMenuButton.onClick.AddListener(OnBottonClicked);
            _secondBuildLevelButton.onClick.AddListener(OnBottonClicked);
            _goBuiltLevel1Button.onClick.AddListener(OnBottonClicked);
            _getRewardTrain1Button.onClick.AddListener(OnBottonClicked);
        }

        private void OnDisable()
        {
            _secondBuildLevelButton.onClick.RemoveListener(OnBottonClicked);
            _openTrainMenuButton.onClick.RemoveListener(OnBottonClicked);
            _goBuiltLevel1Button.onClick.RemoveListener(OnBottonClicked);
            _getRewardTrain1Button.onClick.RemoveListener(OnBottonClicked);
        }

        private void Hide()
        {
            _thirdText.DisableGroup();
            _finger.gameObject.SetActive(false);
            _bildLevel3Button.gameObject.SetActive(true);
            _getRewardTrain2Button2.gameObject.SetActive(true);
            _openTrainMenuButton.gameObject.SetActive(true);
            _openTrain2MenuButton.gameObject.SetActive(true);
        }

        private void OnBottonClicked() => _buttonCliked = true;

        private void StartTutorial()
        {
            StartCoroutine(Tutoring());

            IEnumerator Tutoring()
            {
                _globalMapScroller.enabled = false;

                if (PlayerPrefs.HasKey(SecondBuildLevelClick) == false)
                {
                    Show(_firstText, _secondBuildLevelButton.transform, 4.3f);
                    _openTrainMenuButton.gameObject.SetActive(false);
                    _getRewardTrain1Button.gameObject.SetActive(false);
                    yield return new WaitUntil(() => _buttonCliked);
                    _finger.gameObject.SetActive(false);
                    _firstText.DisableGroup();
                    PlayerPrefs.SetString(SecondBuildLevelClick, SecondBuildLevelClick);
                }
                else if (PlayerPrefs.HasKey(FirstBuiltLevelClick) == false)
                {
                    Show(_secondText, _openTrainMenuButton.transform, 1.6f);
                    _getRewardTrain1Button.gameObject.SetActive(false);
                    _getRewardTrain2Button2.gameObject.SetActive(false);
                    _openTrain2MenuButton.gameObject.SetActive(false);
                    _bildLevel3Button.gameObject.SetActive(false);
                    MoveCamera(_openTrainMenuButton.transform, 4.3f);

                    yield return new WaitUntil(() => _buttonCliked);
                    Show(_secondPart2Text, _goBuiltLevel1Button.transform, 0);
                    _buttonCliked = false;
                    _secondText.DisableGroup();
                    _closeMenuButton.gameObject.SetActive(false);
                    _openTrainMenuButton.gameObject.SetActive(false);
                    yield return new WaitUntil(() => _buttonCliked);
                    PlayerPrefs.SetString(FirstBuiltLevelClick, FirstBuiltLevelClick);
                    _finger.gameObject.SetActive(false);
                    _secondPart2Text.DisableGroup();
                }
                else if (PlayerPrefs.HasKey(GetRewardlClick) == false)
                {
                    Show(_thirdText, _getRewardTrain1Button.transform, 1f);
                    MoveCamera(_getRewardTrain1Button.transform, 1f);
                    _bildLevel3Button.gameObject.SetActive(false);
                    _openTrain2MenuButton.gameObject.SetActive(false);
                    _openTrainMenuButton.gameObject.SetActive(false);
                    _getRewardTrain2Button2.gameObject.SetActive(false);
                    yield return new WaitUntil(() => _buttonCliked);
                    Hide();
                    PlayerPrefs.SetString(GetRewardlClick, GetRewardlClick);
                    yield return new WaitForSeconds(1f);
                }

                if (PlayerPrefs.HasKey(ScrollMap) == false && PlayerPrefs.HasKey(GetRewardlClick))
                {
                    _globalMapScroller.enabled = true;
                    _fourthText.EnableFade();
                    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
                    _fourthText.DisableFade();
                    PlayerPrefs.SetString(ScrollMap, ScrollMap);
                }
            }
        }

        private void Show(CanvasGroup canvas, Transform position, float delay)
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            canvas.EnableFade();
            _finger.gameObject.SetActive(true);
            _position = position;
            Animate();
            _coroutine = StartCoroutine(Moving(delay));

            IEnumerator Moving(float delay)
            {
                var wait = new WaitForFixedUpdate();
                var delayWait = new WaitForSecondsRealtime(delay);
                bool oneTimewait = false;

                while (true)
                {
                    if(oneTimewait == false)
                        yield return delayWait;
                    
                    yield return wait;
                    SetFingetPosition(_position.position);
                    oneTimewait = true;
                }
            }
        }

        private void MoveCamera(Transform position, float delay)
        {
            var newPosition = new Vector3(position.position.x, _camera.transform.position.y, position.position.z);
            _camera.DOMove(newPosition, 0.5f).SetDelay(delay);
        }

        private void SetFingetPosition(Vector3 position) => _finger.position = position;

        private void Animate()
            => _finger.DOPunchScale(Vector3.one * OunchScaleFactor, Duration, 0, 0).SetLoops(-1);
    }
}

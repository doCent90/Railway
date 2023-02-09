using UnityEngine;
using UnityEngine.UI;
using Source.Extensions;
using System.Collections;
using TMPro;

namespace Source.UI
{
    public class MapButtonTutor : MonoBehaviour
    {
        private const string First = "The map will be opened after completing this level";
        private const string Finish = "Congratulations. Tap to open map!";
        private const string Done = "Done";

        [SerializeField] private GameUI _gameUI;
        [SerializeField] private Button _button;
        [SerializeField] private Button _buttonMap;
        [SerializeField] private CanvasGroup _dialogText;
        [SerializeField] private TMP_Text _text;

        private bool _isActive = false;

        private void Start()
        {
            _button.onClick.AddListener(OnCliked);
            _dialogText.DisableGroup(0);
            _buttonMap.interactable = false;

            if (PlayerPrefs.HasKey(Done))
            {
                _button.gameObject.SetActive(false);
                _buttonMap.interactable = true;
            }

            if (_gameUI.TrainMovement == null)
                return;

            StartCoroutine(WaitingEndLevel());
        }

        private void OnDisable() => _button.onClick.RemoveListener(OnCliked);

        private void LateUpdate()
        {
            if (Input.GetMouseButtonDown(0))
                _dialogText.DisableGroup();
        }

        private void OnCliked()
        {
            _text.text = First;
            _dialogText.EnableGroup();
            _dialogText.DelayedDisableGroup(0.3f, 3f);
        }

        private IEnumerator WaitingEndLevel()
        {
            yield return new WaitUntil(() => _gameUI.TrainMovement.FinishedMovement);
            _text.text = Finish;
            _button.gameObject.SetActive(false);
            _buttonMap.interactable = true;
            _dialogText.EnableGroup();

            PlayerPrefs.SetString(Done, Done);
        }
    }
}

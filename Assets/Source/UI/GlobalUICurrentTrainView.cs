using TMPro;
using UnityEngine;
using Source.Extensions;
using UnityEngine.UI;

namespace Source.UI
{
    public class GlobalUICurrentTrainView : MonoBehaviour, ICurrentTrainView
    {
        private const float Minute = 60f;

        [SerializeField] private Button _closeButton;
        [SerializeField] private Button _loadLevelButton;
        [SerializeField] private CanvasGroup _frame;
        [SerializeField] private TMP_Text _currentIncome;
        [SerializeField] private TMP_Text _maxStackIncome;

        private IButtonUI _buttonLevelLoad;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Hide);
            _loadLevelButton.onClick.AddListener(OnLoadLevelClicked);
            _frame.DisableGroup(0);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(Hide);
            _loadLevelButton.onClick.RemoveListener(OnLoadLevelClicked);
        }

        public void Show(int maxStackIncome, IButtonUI buttonLevelLoad)
        {
            float income = Mathf.Round((maxStackIncome / Minute) * 10.0f) * 0.1f;
            _currentIncome.text = $"{income}/s";
            _maxStackIncome.text = maxStackIncome.ToString();
            _buttonLevelLoad = buttonLevelLoad;
            _frame.EnableGroup();
        }

        private void Hide() => _frame.DisableGroup();

        private void OnLoadLevelClicked()
        {
            Hide();
            _buttonLevelLoad.LoadLevel();
        }
    }
}

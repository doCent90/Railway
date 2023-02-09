using System;
using Source.Map.InteractableObjects;
using Source.Progress.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class TaskView : MonoBehaviour, ILocationView
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _currentText;
        [SerializeField] private TextMeshProUGUI _targetText;
        [SerializeField] private MonoBehaviour _locationViewBehaviour;

        private readonly string _pattern = "{0}m";
        private ITask _stackablesTask;
        private ILocationView _locationView => (ILocationView)_locationViewBehaviour;

        private void OnValidate()
        {
            if (_locationViewBehaviour != null && _locationViewBehaviour is ILocationView == false)
            {
                Debug.LogWarning(nameof(_locationViewBehaviour) + " needs to implement " + nameof(ILocationView));
                _locationViewBehaviour = null;
            }
        }

        public void SetLocation(LocationType location)
        {
            _locationView.SetLocation(location);
        }

        public void SetTask(ITask stackablesTask)
        {
            _stackablesTask = stackablesTask ?? throw new ArgumentException();
        }

        private void Update()
        {
            if (_stackablesTask == null)
            {
                Hide();
                return;
            }

            _slider.gameObject.SetActive(_stackablesTask.CurrentProgress != 0);
            _slider.normalizedValue = _stackablesTask.NormalizedProgress;
            int current = Mathf.RoundToInt(_stackablesTask.CurrentProgress * 10) / 10;
            _currentText.text = string.Format(_pattern, current);
            _targetText.text = string.Format(_pattern, _stackablesTask.TargetProgress);
        }

        public void Hide()
        {
            _slider.gameObject.SetActive(false);
            enabled = false;
        }

        public void Show()
        {
            _slider.gameObject.SetActive(true);
            enabled = true;
        }
    }
}

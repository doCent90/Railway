using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class GlobalMapScroller : MonoBehaviour, IScrollRoot
    {
        [SerializeField] private Transform _mapContainer;
        [SerializeField] private CameraBounds _bounds;

        [Header("Touch Sens")]
        [SerializeField] private float _touchMoveSensitivity = 11f;
        [SerializeField] private float _touchZoomSensitivity = 0.01f;

        [Header("Mouse Sens")]
        [SerializeField] private float _mouseMoveSensitivity = 11f;
        [SerializeField] private float _mouseZoomSensitivity = 1f;

        private TouchInput _touchInput;
        private MouseInput _mouseInput;
        private ScrollRoot _scrollRoot;
        private Camera _camera;
        private IInput _currentInput;

        private void Start()
        {
            _scrollRoot = new ScrollRoot(_mapContainer, _camera, _bounds);
#if UNITY_EDITOR
            _mouseInput = new MouseInput(_mouseMoveSensitivity, _mouseZoomSensitivity, _camera);
            _currentInput = _mouseInput;
#endif
#if !UNITY_EDITOR
            _touchInput = new TouchInput(_touchMoveSensitivity, _touchZoomSensitivity, _camera);
            _currentInput = _touchInput;
#endif
            _currentInput.StartedClick += OnStartedClick;
            _currentInput.EndedClick += OnEndedClick;
            _currentInput.Zooming += OnZooming;
            _currentInput.Moving += OnMoving;
        }

        private void Update()
        {
#if UNITY_EDITOR
            _mouseInput.Update();
#endif
#if !UNITY_EDITOR
            _touchInput.Update();
#endif
        }

        private void OnDisable()
        {
            _currentInput.StartedClick -= OnStartedClick;
            _currentInput.EndedClick -= OnEndedClick;
            _currentInput.Zooming -= OnZooming;
            _currentInput.Moving -= OnMoving;
        }

        public void Construct(Camera camera) => _camera = camera;

        public void OnStartedClick(Vector2 position) => _scrollRoot.OnStartedClick(position);
        public void OnEndedClick(Vector2 position) => _scrollRoot.OnEndedClick(position);
        public void OnZooming(float value) => _scrollRoot.OnZooming(value);
        public void OnMoving(Vector2 position) => _scrollRoot.OnMoving(position);
    }
}

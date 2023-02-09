using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.UI
{
    public class ScrollRoot : IScrollRoot
    {
        private const float Epsilon = 50f;
        private const string LayerName = "Button";

        private readonly Transform _transformMap;
        private readonly CameraBounds _cameraBounds;
        private readonly Camera _camera;

        private Vector2 _screenModifier;
        private Vector2 _startClickPosition;
        private float _currentZoom;

        private float _zoomModifier => _camera.transform.position.y * 0.1f;
        public float NormalizedCurrentZoom => Mathf.Abs(_cameraBounds.NearZoomLimit - _currentZoom) / Mathf.Abs(_cameraBounds.NearZoomLimit - _cameraBounds.FarZoomLimit);

        public ScrollRoot(Transform transformMap, Camera camera, CameraBounds bounds)
        {
            _transformMap = transformMap;
            _camera = camera;
            _cameraBounds = bounds;
            _screenModifier = new Vector2((float)_camera.pixelWidth / _camera.pixelHeight, 1);
            _currentZoom = _camera.orthographicSize;
        }

        public void OnStartedClick(Vector2 screenPosition)
        {
            _startClickPosition = screenPosition;
        }

        public void OnMoving(Vector2 deltaPosition)
        {
            Vector3 newCameraPosition = _camera.transform.position + ConvertInputPosition(deltaPosition);
            newCameraPosition.x = Mathf.Clamp(newCameraPosition.x, _cameraBounds.Left, _cameraBounds.Right);
            newCameraPosition.z = Mathf.Clamp(newCameraPosition.z, _cameraBounds.Bottom, _cameraBounds.Top);

            _camera.transform.position = newCameraPosition;
        }

        public void OnEndedClick(Vector2 screenPosition)
        {
            if ((screenPosition - _startClickPosition).magnitude > Epsilon)
                return;

            if (IsPointerOverUIObject(screenPosition))
                return;
        }

        public void OnZooming(float zoomDelta)
        {
            if (zoomDelta == 0)
                return;

            _currentZoom = Mathf.Clamp(_currentZoom + zoomDelta, _cameraBounds.NearZoomLimit, _cameraBounds.FarZoomLimit);

            _camera.orthographicSize = _currentZoom;
        }

        private bool IsPointerOverUIObject(Vector2 inputPosition)
        {
            PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
            eventDataCurrentPosition.position = inputPosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

            for (int i = 0; i < results.Count; i++)
                if (results[i].gameObject.layer == LayerMask.NameToLayer(LayerName))
                    return true;

            return false;
        }

        private Vector3 ConvertInputPosition(Vector2 inputPosition)
            => new Vector3(inputPosition.x * _screenModifier.x, 0, inputPosition.y * _screenModifier.y) * _zoomModifier;
    }
}

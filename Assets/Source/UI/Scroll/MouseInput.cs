using System;
using UnityEngine;

namespace Source.UI
{
    public class MouseInput : UserInput, IInput
    {
        private Vector3 _lastPosition;

        public event Action<float> Zooming;
        public event Action<Vector2> StartedClick;
        public event Action<Vector2> Moving;
        public event Action<Vector2> EndedClick;

        public MouseInput(float moveSensitivity, float zoomSensitivity, Camera camera) : base(moveSensitivity, zoomSensitivity, camera) { }

        public void Update()
        {
            Zooming?.Invoke(-Math.Sign(Input.mouseScrollDelta.y) * ZoomSensitivity);

            if (Input.GetMouseButtonDown(0))
            {
                _lastPosition = Input.mousePosition;
                StartedClick?.Invoke(_lastPosition);
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 inputPosition = Input.mousePosition;
                Moving?.Invoke(CalculateDeltaPosition(inputPosition));
                _lastPosition = inputPosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                EndedClick?.Invoke(Input.mousePosition);
            }
        }

        private Vector2 CalculateDeltaPosition(Vector3 inputPosition) => -(Camera.ScreenToViewportPoint(inputPosition - _lastPosition) * MoveSensitivity);
    }

}

using System;
using UnityEngine;

namespace Source.UI
{
    public class TouchInput : UserInput, IInput
    {
        private Touch _firstTouch;
        private Touch _secondTouch;

        public event Action<float> Zooming;
        public event Action<Vector2> StartedClick;
        public event Action<Vector2> Moving;
        public event Action<Vector2> EndedClick;

        public TouchInput(float moveSensitivity, float zoomSensitivity, Camera camera) : base(moveSensitivity, zoomSensitivity, camera) { }

        public void Update()
        {
            if (Input.touchCount == 0)
                return;

            if (Input.touchCount < 2)
            {
                var touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        StartedClick?.Invoke(touch.position);
                        break;
                    case TouchPhase.Moved:
                        Moving?.Invoke(CalculateDeltaPosition(touch));
                        break;
                    case TouchPhase.Ended:
                        EndedClick?.Invoke(touch.position);
                        break;
                }
            }
            else
            {
                var newFirstTouch = Input.GetTouch(0);
                var newSecondTouch = Input.GetTouch(1);

                if (newFirstTouch.phase == TouchPhase.Began || newSecondTouch.phase == TouchPhase.Began)
                {
                    _firstTouch = newFirstTouch;
                    _secondTouch = newSecondTouch;
                    return;
                }

                float startDistance = (_firstTouch.position - _secondTouch.position).magnitude;
                float newDistance = (newFirstTouch.position - newSecondTouch.position).magnitude;
                float offset = startDistance - newDistance;

                Zooming?.Invoke(offset * ZoomSensitivity);

                _firstTouch = newFirstTouch;
                _secondTouch = newSecondTouch;
            }
        }

        private Vector2 CalculateDeltaPosition(Touch touch) => -(Camera.ScreenToViewportPoint(touch.deltaPosition) * MoveSensitivity);
    }

}

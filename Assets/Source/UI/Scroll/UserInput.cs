using UnityEngine;

namespace Source.UI
{
    public abstract class UserInput
    {
        protected float MoveSensitivity;
        protected float ZoomSensitivity;
        protected Camera Camera;

        protected UserInput(float moveSensitivity, float zoomSensitivity, Camera camera)
        {
            MoveSensitivity = moveSensitivity;
            ZoomSensitivity = zoomSensitivity;
            Camera = camera;
        }
    }
}

using System;
using UnityEngine;

namespace Source.UI
{
    public interface IInput
    {
        event Action<float> Zooming;
        event Action<Vector2> StartedClick;
        event Action<Vector2> Moving;
        event Action<Vector2> EndedClick;

        void Update();
    }

}

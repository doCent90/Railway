using UnityEngine;
using System;

namespace Source.UI
{
    [Serializable]
    public class CameraGlobalUISetter
    {
        [SerializeField] private Canvas _canvas;

        public void Init(Camera camera) => _canvas.worldCamera = camera;
    }
}

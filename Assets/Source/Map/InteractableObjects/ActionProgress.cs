using System;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    [Serializable]
    public class ActionProgress
    {
        private const float Tolerance = Single.Epsilon;

        [SerializeField] private float _targetProgress;
        [SerializeField] private float _progress;

        public bool Completed => Math.Abs(_targetProgress - _progress) < Tolerance;
        public float NormalizedProgress => _progress / _targetProgress;
        public float TargetProgress => _targetProgress;

        public void Complete()
        {
            AddProgress(_targetProgress);
        }

        public void AddProgress(float deltaTime)
        {
            if (deltaTime < 0)
                throw new ArgumentException();

            _progress = Mathf.Clamp(_progress + deltaTime, 0, _targetProgress);
        }
    }
}

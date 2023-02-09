using System;
using UnityEngine;

namespace Source.Progress.Tasks
{
    [Serializable]
    public class DistanceTask : ITask
    {
        [SerializeField] private int _targetProgress;
        [SerializeField] private float _distance;

        public DistanceTask(int targetProgress)
        {
            if (targetProgress <= 0)
                throw new ArgumentException();

            _targetProgress = targetProgress;
        }

        public float NormalizedProgress => Mathf.Clamp(CurrentProgress / TargetProgress, 0, 1);
        public float CurrentProgress => Mathf.Clamp(_distance, 0, TargetProgress);
        public int TargetProgress => _targetProgress;

        public void SetDistance(float distanceTraveled)
        {
            _distance = distanceTraveled;
        }
    }
}

using System;
using Source.Progress.Counters;
using Source.Stack;
using UnityEngine;

namespace Source.Progress.Tasks
{
    public class CollectStackablesTask : ITask
    {
        private readonly StackablesCounter _counter;
        private readonly StackableType _stackableType;
        private readonly int _targetProgress;

        public CollectStackablesTask(StackablesCounter counter, StackableType stackableType, int targetProgress)
        {
            if (targetProgress < 0)
                throw new ArgumentException();
            
            _counter = counter?? throw new ArgumentException();
            _stackableType = stackableType;
            _targetProgress = targetProgress;
        }

        public float NormalizedProgress => CurrentProgress / TargetProgress;
        public float CurrentProgress => Mathf.Clamp(_counter.GetAmount(_stackableType), 0, TargetProgress);
        public int TargetProgress => _targetProgress;

        public ITaskView TaskView { get; }
    }
}
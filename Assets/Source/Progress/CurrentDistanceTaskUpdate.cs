using System;
using System.Collections;
using Source.Common;
using Source.Progress.Tasks;
using Source.SaveLoad;
using Source.UI;
using UnityEngine;

namespace Source.Progress
{
    public class CurrentDistanceTaskUpdate
    {
        private readonly TaskView _taskView;
        private readonly DistanceTask[] _distanceTasks;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly DistanceTraveled _distanceTraveled;

        private DistanceTask _currentTask;

        public CurrentDistanceTaskUpdate(DistanceTraveled distanceTraveled, TaskView taskView,
            ICoroutineRunner coroutineRunner,
            DistanceTask[] levelDataDistanceTasks)
        {
            _distanceTraveled = distanceTraveled;
            _coroutineRunner = coroutineRunner;
            _distanceTasks = levelDataDistanceTasks;
            _taskView = taskView;
        }

        public void Start()
        {
            _currentTask = _distanceTasks[0];
            _taskView.SetTask(_currentTask);
            _coroutineRunner.StartCoroutine(ReplaceTask());
        }

        public void Update() =>
            _currentTask?.SetDistance(_distanceTraveled.Distance);

        private IEnumerator ReplaceTask()
        {
            while (true)
            {
                yield return new WaitUntil(() => _currentTask.NormalizedProgress >= 1);
                DistanceTask[] tasks = _distanceTasks;
                DistanceTask next = tasks[Mathf.Clamp(Array.IndexOf(tasks, _currentTask) + 1, 0, tasks.Length - 1)];

                if (next.CurrentProgress == 0)
                    next.SetDistance(_distanceTraveled.Distance);

                _currentTask = next;
                _taskView.Hide();
                yield return new WaitForSeconds(20f);
                _taskView.SetTask(_currentTask);

                if (_currentTask.NormalizedProgress < 1)
                    _taskView.Show();
                else
                    break;
            }

            _currentTask = null;
        }
    }
}

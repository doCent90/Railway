using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Characters.Worker
{
    [CreateAssetMenu(menuName = "Create WorkersList", fileName = "WorkersList", order = 0)]
    public class WorkersList : ScriptableObject
    {
        [SerializeField] private List<WorkerConfig> _workerConfigs;

        public IEnumerable<WorkerConfig> WorkerConfigs => _workerConfigs;
        public IEnumerable<WorkerStats> WorkerStats => _workerConfigs.Select(config => config.WorkerStats);
    }

    [Serializable]
    public class WorkerConfig
    {
        [field: SerializeField] public Worker WorkerTemplate { get; private set; }
        [field: SerializeField] public WorkerStats WorkerStats { get; private set; }
    }
}
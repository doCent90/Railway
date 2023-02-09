using System;
using System.Linq;
using Source.Characters.Behaviour;
using Source.Characters.Worker.Merge.GenericMerge;
using Source.Map.InteractableObjects.Cells;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Characters.Worker.Merge
{
    public class MergeableWorkerFactory : IMergeableFactory<WorkerStats, Worker>
    {
        private readonly WorkersList _workersList;
        private readonly InteractableObjectsContainer _resourcesContainer;
        private readonly InteractableObjectsContainer _railwayContainer;
        private readonly Transform[] _wayPoints;
        private readonly Transform _spawnPoint;
        private readonly CellWay _cellWay;

        public MergeableWorkerFactory(InteractableObjectsContainer railwayContainer,
            InteractableObjectsContainer resourcesContainer,
            WorkersList workersList, Transform spawnPoint, CellWay cellWay, params Transform[] wayPoints)
        {
            _cellWay = cellWay ?? throw new ArgumentException();
            _spawnPoint = spawnPoint ? spawnPoint : throw new ArgumentException();
            _wayPoints = wayPoints ?? throw new ArgumentException();
            _railwayContainer = railwayContainer ?? throw new ArgumentException();
            _resourcesContainer = resourcesContainer ?? throw new ArgumentException();
            _workersList = workersList ? workersList : throw new ArgumentException();
        }

        public Worker Create(WorkerStats stats)
        {
            Worker template = _workersList.WorkerConfigs.First(config => config.WorkerStats == stats).WorkerTemplate;
            Worker worker = Object.Instantiate(template, _spawnPoint.transform.position, _spawnPoint.rotation);
            worker.Construct(_wayPoints, _railwayContainer, _resourcesContainer, stats, _cellWay);

            return worker;
        }
    }
}

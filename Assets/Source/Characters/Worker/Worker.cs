using System;
using BehaviorDesigner.Runtime;
using Source.Characters.Behaviour;
using Source.Map.InteractableObjects.Cells;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Characters.Worker
{
    public class Worker : MonoBehaviour
    {
        [SerializeField] private BehaviorTree _behaviorTree;
        [SerializeField] private CharacterMovementSpeedUpdater _speedUpdater;
        [SerializeField] private Animation _merge;

        private InteractableObjectsContainer _interactableObjectsContainer;
        private InteractableObjectsContainer _resourcesContainer;

        public Transform[] WaitPoints { get; private set; }
        public WorkerStats Stats { get; set; }

        public void Construct(Transform[] waitPoints, InteractableObjectsContainer interactablesContainer,
            InteractableObjectsContainer resourcesContainer, WorkerStats workerStats, CellWay cellWay)
        {
            Stats = workerStats ? workerStats : throw new ArgumentException();
            WaitPoints = waitPoints ?? throw new ArgumentException();
            _interactableObjectsContainer = interactablesContainer ?? throw new ArgumentException();
            _resourcesContainer = resourcesContainer ?? throw new ArgumentException();
            _speedUpdater.Construct(Stats);
            _behaviorTree.SetVariableValue("_interactablesContainer", _interactableObjectsContainer);
            _behaviorTree.SetVariableValue("_resourcesContainer", _resourcesContainer);
            _behaviorTree.SetVariableValue("_cellWay", cellWay);
        }

        public Vector3 GetRandomWaypoint() =>
            WaitPoints[Random.Range(0, WaitPoints.Length)].position;

        public void Merge()
        {
            _merge.Play();
        }
    }
}

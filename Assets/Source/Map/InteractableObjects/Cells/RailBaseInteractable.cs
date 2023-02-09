using System;
using DG.Tweening;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects.Cells
{
    public class RailBaseInteractable : BuildableCell, ICharacterInteractable
    {
        [SerializeField] private ProgressObjectActivator _progressObjectActivator;
        [SerializeField] private BuildableCell _childBuildableCell;
        [SerializeField] private int _interactionsCount;
        [SerializeField] private StackableType _requiredStackableType;
        [SerializeField] private MonoBehaviour _interactConditionBehaviour;

        private IInteractCondition _interactCondition;
        private float _lastInteraction;
        private float _interactProgress;
        private float _interactionThreshold => 1f / _interactionsCount;

        private void Awake()
        {
            _interactCondition = _interactConditionBehaviour
                ? (IInteractCondition) _interactConditionBehaviour
                : new NullInteractCondition();
            
            _progressObjectActivator.Display(transform, Progress.NormalizedProgress);
        }

        public bool CanInteract(StackPresenter enteredStack) => 
            enteredStack.CanRemoveFromStack(_requiredStackableType) && CanInteract();

        public bool CanInteract(StackableType stackableType) =>
            _requiredStackableType == stackableType && CanInteract();

        private bool CanInteract() =>
            _interactProgress < 1 && Progress.Completed == false && CheckChildCellCondition() && _interactCondition.CanInteract();

        private bool CheckChildCellCondition() =>
            _childBuildableCell == null ||
            _childBuildableCell.Progress.Completed;

        public Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition) =>
            transform.position + (transformPosition.position - transform.position).normalized;


        public void Interact(StackPresenter enteredStack, float deltaTime)
        {
            if (CanInteract(enteredStack) == false)
                throw new InvalidOperationException();

            _interactProgress += Mathf.Clamp(deltaTime / Progress.TargetProgress, 0, _interactionThreshold);

            if (_interactProgress >= Mathf.Clamp01(_lastInteraction + _interactionThreshold))
            {
                _lastInteraction += _interactionThreshold;
                InteractWithStack(enteredStack);
            }
        }

        private void InteractWithStack(StackPresenter enteredStack)
        {
            Stackable stackable = enteredStack.RemoveFromStack(_requiredStackableType);
            stackable.transform.DOMove(transform.position, 0.3f).OnComplete(() => AddStackable(stackable));
        }

        private void AddStackable(Stackable stackable)
        {
            Progress.AddProgress(Progress.TargetProgress * _interactionThreshold);
            _progressObjectActivator.Display(stackable.transform, Progress.NormalizedProgress);
            Destroy(stackable.gameObject);
        }
    }
}
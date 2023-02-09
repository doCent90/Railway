using System.Collections.Generic;
using System.Linq;
using Source.Characters.Behaviour.Interactable;
using Source.Characters.Worker.View;
using Source.Map.ChunksLoader;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects.Cells
{
    public class Cell : MonoBehaviour, ICharacterInteractable, IInteractablesList, ICellLock
    {
        [SerializeField] private RailInteractable _cell;
        [SerializeField] private List<MonoBehaviour> _interactableBehaviours;
        [SerializeField] private LocationView locationView;
        [SerializeField] private bool _lockNextCells;
        [SerializeField] private MonoBehaviour _cellLock;

        private List<ICharacterInteractable> _interactables;
        private ICellLock _lastCellLock;
        private ICellLock _currentCellLock;

        public bool Completed => _cell.Progress.Completed;
        public bool Locked => _lastCellLock.Locked || _lockNextCells && _currentCellLock.Locked;

        public void Construct(ICellLock lastCellLock, LocationType location)
        {
            _lastCellLock = lastCellLock;
            locationView.SetLocation(location);
        }

        private void OnValidate()
        {
            if (_cellLock != null && _cellLock is ICellLock == false)
            {
                _cellLock = null;
                Debug.LogWarning(_cellLock + "needs to implement" + nameof(ICellLock));
            }
        }

        private void Awake()
        {
            _currentCellLock = _cellLock != null ? (ICellLock) _cellLock : new AlwaysUnlocked();
            _interactables = _interactableBehaviours.Cast<ICharacterInteractable>().ToList();
        }

        public bool CanInteract(StackPresenter enteredStack) =>
            !Locked && _interactables.Any(interactable => interactable.CanInteract(enteredStack));

        public bool CanInteract(StackableType stackableType) =>
            !Locked && _interactables.Any(interactable => interactable.CanInteract(stackableType));

        public void Interact(StackPresenter enteredStack, float deltaTime) =>
            FirstInteractable(enteredStack).Interact(enteredStack, deltaTime);

        public ICharacterInteractable FirstInteractable(StackPresenter enteredStack) =>
            _interactables.First(interactable => interactable.CanInteract(enteredStack));

        public Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition)
        {
            if (_interactables.Any(interactable => interactable.CanInteract(stackPresenter)))
                return _interactables.First(interactable => interactable.CanInteract(stackPresenter))
                    .GetInteractPoint(stackPresenter, transformPosition);

            return transform.position + (transformPosition.position - transform.position).normalized;
        }
    }
}

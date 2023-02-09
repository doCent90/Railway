using System.Collections.Generic;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;
using UnityEngine;
using Random = System.Random;

namespace Source.Characters.Train
{
    public class StackFulfillInteractable : MonoBehaviour, ICharacterInteractable
    {
        [SerializeField] private StackPresenter _stackPresenter;
        [SerializeField] private Transform _rightInteractPoint;
        [SerializeField] private Transform _leftInteractPoint;
        [SerializeField] private Transform[] _rightInteractPoints;
        [SerializeField] private Transform[] _leftInteractPoints;

        public bool CanInteract(StackPresenter enteredStack)
        {
            bool canAdd = false;

            foreach (Stackable stackable in enteredStack.Data)
                canAdd |= CanInteract(stackable.Type);

            return canAdd;
        }

        public bool CanInteract(StackableType stackableType) =>
            _stackPresenter.CanAddToStack(stackableType);

        public void Interact(StackPresenter enteredStack, float deltaTime)
        {
            IEnumerable<Stackable> interactables = enteredStack.RemoveAll();

            foreach (Stackable stackable in interactables)
            {
                if(CanInteract(stackable.Type))
                    _stackPresenter.AddToStack(stackable);
            }
        }

        public Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition)
        {
            float distanceToRight = Vector3.SqrMagnitude(transformPosition.position - _rightInteractPoint.position);
            float distanceToLeft = Vector3.SqrMagnitude(transformPosition.position - _leftInteractPoint.position);

            Transform[] selectedArray = _rightInteractPoints;

            if (distanceToRight > distanceToLeft)
                selectedArray = _leftInteractPoints;

            return GetForArray(transformPosition, selectedArray);
        }

        private Vector3 GetForArray(Transform transformPosition, Transform[] interactPoints)
        {
            Random random = new Random(transformPosition.gameObject.GetHashCode());
            Vector3 interactPosition = interactPoints[random.Next(0, interactPoints.Length)].position;

            return interactPosition + (transformPosition.position - interactPosition).normalized;
        }
    }
}

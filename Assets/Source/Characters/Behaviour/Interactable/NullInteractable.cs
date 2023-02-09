using Source.Stack;
using UnityEngine;

namespace Source.Characters.Behaviour.Interactable
{
    public class NullInteractable : ICharacterInteractable
    {
        public bool CanInteract(StackPresenter enteredStack) =>
            false;

        public bool CanInteract(StackableType stackableType) =>
            false;

        public void Interact(StackPresenter enteredStack, float deltaTime)
        {
        }

        public Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition) => 
            stackPresenter.transform.position;
    }
}
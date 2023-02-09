using Source.Stack;
using UnityEngine;

namespace Source.Characters.Behaviour.Interactable
{
    public interface ICharacterInteractable
    {
        bool CanInteract(StackPresenter enteredStack);
        bool CanInteract(StackableType stackableType);
        void Interact(StackPresenter enteredStack, float deltaTime);
        Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition);
    }
}
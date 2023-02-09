using Source.Characters.Behaviour;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;

namespace Source.Characters.Worker.View
{
    public interface IInteractablesList
    {
        ICharacterInteractable FirstInteractable(StackPresenter enteredStack);
    }
}
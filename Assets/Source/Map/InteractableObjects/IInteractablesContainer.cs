using Source.Characters.Behaviour.Interactable;

namespace Source.Map.InteractableObjects
{
    public interface IInteractablesContainer
    {
        void Add(ICharacterInteractable interactable);
        void Remove(ICharacterInteractable interactable);
    }
}
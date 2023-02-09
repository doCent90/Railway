using System;
using Source.Characters.Behaviour;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;

namespace Source.Map.InteractableObjects
{
    class ObjectContainerInteractableProvider : InteractableProvider
    {
        private InteractableObjectsContainer _interactableObjectsContainer;

        public void Construct(InteractableObjectsContainer interactableObjectsContainer)
        {
            _interactableObjectsContainer = interactableObjectsContainer ?? throw new ArgumentNullException();
        }

        public override bool TryGetInteractable(StackPresenter stack, out ICharacterInteractable characterInteractable)
        {
            characterInteractable = _interactableObjectsContainer.FirstOrDefault(interactable =>
                interactable.CanInteract(stack));

            return characterInteractable != null;
        }
    }
}

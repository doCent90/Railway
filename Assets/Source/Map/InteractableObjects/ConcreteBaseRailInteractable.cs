using Source.Characters.Behaviour.Interactable;
using Source.Map.InteractableObjects.Cells;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    class ConcreteBaseRailInteractable : InteractableProvider
    {
        [SerializeField] private RailBaseInteractable _railBaseInteractable;
        
        public override bool TryGetInteractable(StackPresenter stack, out ICharacterInteractable characterInteractable)
        {
            characterInteractable = _railBaseInteractable;

            return _railBaseInteractable != null;
        }
    }
}
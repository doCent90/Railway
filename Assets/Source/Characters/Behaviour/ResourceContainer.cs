using System.Collections.Generic;
using System.Linq;
using Source.Stack;

namespace Source.Characters.Behaviour
{
    class ResourceContainer : InteractableObjectsContainer
    {
        private readonly StackableTypes _allStackableTypes;
        private readonly InteractableObjectsContainer _railwayInteractables;

        public ResourceContainer(InteractableObjectsContainer interactableObjectsContainer,
            StackableTypes allStackableTypes)
        {
            _allStackableTypes = allStackableTypes; 
            _railwayInteractables = interactableObjectsContainer;
        }

        public override bool HasTargetFor(StackPresenter stackPresenter)
        {
            IEnumerable<StackableType> interactables =
                _allStackableTypes.Value.Where(type => base.HasTargetFor(stackPresenter));

            return _railwayInteractables.Has(interactable => interactables.Any(interactable.CanInteract));
        }
    }
}
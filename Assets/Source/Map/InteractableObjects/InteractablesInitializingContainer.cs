using System;
using Source.Characters.Behaviour;
using Source.Characters.Behaviour.Interactable;
using Source.Map.InteractableObjects.Priority;

namespace Source.Map.InteractableObjects
{
    class InteractablesInitializingContainer : IInteractablesContainer
    {
        private readonly InteractableObjectsContainer _container;
        private readonly IPriorityByNumber _priorityByNumber;

        private int _added;

        public InteractablesInitializingContainer(InteractableObjectsContainer container,
            IPriorityByNumber priorityByNumber)
        {
            _priorityByNumber = priorityByNumber ?? throw new ArgumentException();
            _container = container ?? throw new ArgumentException();
        }

        public void Add(ICharacterInteractable interactable)
        {
            if (_container.Contains(interactable))
                return;

            _added++;
            _container.Add(interactable, _priorityByNumber.GetFor(_added));
        }

        public void Remove(ICharacterInteractable interactable)
        {
            if (_container.Contains(interactable))
                _container.Remove(interactable);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Characters.Behaviour
{
    public class InteractableObjectsContainer
    {
        private readonly List<InteractablePriority> _interactablePriorities = new();

        public bool Contains(ICharacterInteractable interactable) =>
            _interactablePriorities.Any(priority => priority.CharacterInteractable == interactable);

        public void Remove(ICharacterInteractable characterInteractable)
        {
            _interactablePriorities.Remove(_interactablePriorities.First(priority =>
                priority.CharacterInteractable == characterInteractable));
        }

        public void Add(ICharacterInteractable getComponentInChildren, int priority)
        {
            for (var i = 0; i < _interactablePriorities.Count; i++)
            {
                if (_interactablePriorities[i].Priority > priority)
                {
                    _interactablePriorities.Insert(i,
                        new InteractablePriority(getComponentInChildren, priority));
                    return;
                }
            }

            _interactablePriorities.Add(new InteractablePriority(getComponentInChildren, priority));
        }

        public ICharacterInteractable FirstOrDefault(Func<ICharacterInteractable, bool> condition) =>
            _interactablePriorities.FirstOrDefault(priority => condition(priority.CharacterInteractable))
                ?.CharacterInteractable;

        public virtual bool HasTargetFor(StackPresenter stackPresenter) =>
            _interactablePriorities.Any(priority => priority.CharacterInteractable.CanInteract(stackPresenter));

        public bool Has(Func<ICharacterInteractable, bool> func) =>
            _interactablePriorities.Any(priority => func(priority.CharacterInteractable));

        public ICharacterInteractable Get(StackPresenter stackPresenter, Comparison<ICharacterInteractable> comparison)
        {
            int priority = _interactablePriorities
                .Where(interactablePriority => interactablePriority.CharacterInteractable.CanInteract(stackPresenter))
                .Min(interactablePriority => interactablePriority.Priority);

            List<ICharacterInteractable> list =
                _interactablePriorities
                    .Where(interactablePriority => interactablePriority.Priority == priority
                                                   && interactablePriority.CharacterInteractable.CanInteract(
                                                       stackPresenter))
                    .Select(interactablePriority => interactablePriority.CharacterInteractable).ToList();

            list.Sort(comparison.Invoke);
            ICharacterInteractable interactable = list.First();

            if (interactable.CanInteract(stackPresenter) == false)
                throw new InvalidOperationException();

            return interactable;
        }

        public void AddActor(ICharacterInteractable characterInteractable, StackPresenter value)
        {
            _interactablePriorities.First(interactablePriority =>
                interactablePriority.CharacterInteractable == characterInteractable).AddParticipant(value);
        }

        public void RemoveActor(StackPresenter stackPresenter)
        {
            foreach (InteractablePriority priority in _interactablePriorities)
                priority.RemoveParticipant(stackPresenter);
        }
    }

    public class InteractablePriority
    {
        public readonly ICharacterInteractable CharacterInteractable;
        private readonly List<StackPresenter> _participants = new();
        private readonly int _priority;

        public InteractablePriority(ICharacterInteractable characterInteractable, int priority)
        {
            _priority = priority;
            CharacterInteractable = characterInteractable;
        }

        public int Priority => _priority + _participants.Count;

        public void AddParticipant(StackPresenter stackPresenter)
        {
            if (_participants.Contains(stackPresenter) == false)
                _participants.Add(stackPresenter);
        }

        public void RemoveParticipant(StackPresenter stackPresenter)
        {
            if (_participants.Contains(stackPresenter))
                _participants.Remove(stackPresenter);
        }
    }
}
using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.Interactable;
using Source.Characters.Behaviour.SharedVariables;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public class SetCurrentInteractableTarget : Action
    {
        public SharedInteractablesContainer SharedInteractablesContainer;
        public SharedStackPresenter SelfStackPresenter;
        public SharedInteractable CurrentInteractable;

        public override void OnAwake() =>
            CurrentInteractable.Value = new CharacterInteractableReference(new NullInteractable());

        public override TaskStatus OnUpdate()
        {
            ICharacterInteractable characterInteractable = SharedInteractablesContainer.Value.Get(
                SelfStackPresenter.Value, Compare());

            SharedInteractablesContainer.Value.AddActor(characterInteractable, SelfStackPresenter.Value);
            CurrentInteractable.Value = new CharacterInteractableReference(characterInteractable);

            return TaskStatus.Success;
        }

        private Comparison<ICharacterInteractable> Compare() => (interactable, interactable2) =>
            GetSqrMagnitude(interactable) < GetSqrMagnitude(interactable2) ? -1 : 1;

        private float GetSqrMagnitude(ICharacterInteractable interactable) =>
            Vector3.SqrMagnitude(
                interactable.GetInteractPoint(SelfStackPresenter.Value, transform) - transform.position);
    }
}
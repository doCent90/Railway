using BehaviorDesigner.Runtime;
using Source.Characters.Behaviour.SharedVariables;
using UnityEngine;

namespace Source.Characters.Behaviour.Actions
{
    class SetDestimationInteractable : SetDestination
    {
        public SharedInteractable CurrentInteractable;
        public SharedTransform CharacterTransform;
        public SharedStackPresenter SharedStackPresenter;

        protected override Vector3 GetPosition() =>
            CurrentInteractable.Value.CharacterInteractable.GetInteractPoint(SharedStackPresenter.Value, CharacterTransform.Value);
    }
}
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public class GoToInteractable : Action
    {
        public SharedBotCharacterInput BotCharacterInput;
        public SharedInteractable CurrentInteractable;
        public SharedTransform CharacterTransform;
        public SharedStackPresenter SelfStackPresenter;

        private float _treshold = 0.1f;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = GetPosition();

            return Vector3.SqrMagnitude(BotCharacterInput.Value.Destination - transform.position) < _treshold
                ? TaskStatus.Success
                : TaskStatus.Running;
        }

        private Vector3 GetPosition() =>
            CurrentInteractable.Value.CharacterInteractable.GetInteractPoint(SelfStackPresenter.Value,
                CharacterTransform.Value);
    }
}

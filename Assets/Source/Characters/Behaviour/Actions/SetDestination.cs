using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public abstract class SetDestination : Action
    {
        public SharedBotCharacterInput BotCharacterInput;
        public SharedVector3 Destination;

        public override TaskStatus OnUpdate()
        {
            Vector3 destination = GetPosition();

            Destination.Value = destination;

            return TaskStatus.Success;
        }

        protected abstract Vector3 GetPosition();
    }
}
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public class GoTo : Action
    {
        public SharedBotCharacterInput BotCharacterInput;
        public SharedVector3 Destination;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = Destination.Value;

            return TaskStatus.Success;
        }
    }
}
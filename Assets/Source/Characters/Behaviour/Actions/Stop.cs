using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public class Stop : Action
    {
        public SharedBotCharacterInput BotCharacterInput;

        public override TaskStatus OnUpdate()
        {
            BotCharacterInput.Value.Destination = transform.position;
            
            return TaskStatus.Success;
        }
    }
}
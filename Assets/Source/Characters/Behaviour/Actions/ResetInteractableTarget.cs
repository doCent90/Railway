using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public class ResetInteractableTarget : Action
    {
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            CurrentInteractable.Value.Reset();

            return TaskStatus.Success;
        }
    }
}
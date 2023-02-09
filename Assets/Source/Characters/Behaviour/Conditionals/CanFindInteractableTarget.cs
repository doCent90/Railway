using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;

namespace Source.Characters.Behaviour.Conditionals
{
    public class CanFindInteractableTarget : Conditional
    {
        public SharedInteractablesContainer SharedInteractablesContainer;
        public SharedStackPresenter SelfStackPresenter;

        public override TaskStatus OnUpdate()
        {
            bool hasInteractable = SharedInteractablesContainer.Value.HasTargetFor(SelfStackPresenter.Value);

            TaskStatus taskStatus = hasInteractable ? TaskStatus.Success : TaskStatus.Failure;

            return taskStatus;
        }
    }
}
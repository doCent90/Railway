using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;

namespace Source.Characters.Behaviour.Conditionals
{
    public class CanInteract : Conditional
    {
        public SharedStackPresenter SelfStackPresenter;
        public SharedInteractable CurrentInteractable;

        public override TaskStatus OnUpdate()
        {
            bool canInteract = CurrentInteractable.Value.CharacterInteractable.CanInteract(SelfStackPresenter.Value);
            return canInteract ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
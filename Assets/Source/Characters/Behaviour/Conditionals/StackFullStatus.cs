using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;

namespace Source.Characters.Behaviour.Conditionals
{
    public class StackFullStatus : Conditional
    {
        public SharedBool _empty;
        public SharedStackPresenter SelfStackPresenter;

        public override TaskStatus OnUpdate()
        {
            if (_empty.Value)
                return SelfStackPresenter.Value.Empty ? TaskStatus.Success : TaskStatus.Failure;

            return SelfStackPresenter.Value.IsFull ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}
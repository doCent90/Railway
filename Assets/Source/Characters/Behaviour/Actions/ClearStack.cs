using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;
using Source.Stack;
using Action = BehaviorDesigner.Runtime.Tasks.Action;
using Object = UnityEngine.Object;

namespace Source.Characters.Behaviour.Actions
{
    public class ClearStack : Action
    {
        public SharedStackPresenter SelfStackPresenter;
        
        public override TaskStatus OnUpdate()
        {
            if (SelfStackPresenter.Value.Empty == false)
            {
                foreach (Stackable stackable in SelfStackPresenter.Value.RemoveAll())
                    Object.Destroy(stackable.gameObject);
            }

            return TaskStatus.Success;
        }
    }
}
using BehaviorDesigner.Runtime;

namespace Source.Characters.Behaviour.SharedVariables
{
    public class SharedWorker : SharedVariable<Worker.Worker>
    {
        public static implicit operator SharedWorker(Worker.Worker value) =>
            new SharedWorker {Value = value};
    }
}
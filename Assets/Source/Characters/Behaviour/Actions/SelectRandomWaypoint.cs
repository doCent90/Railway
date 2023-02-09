using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;

namespace Source.Characters.Behaviour.Actions
{
    public class SelectRandomWaypoint : Action
    {
        public SharedVector3 Waypoint;
        public SharedWorker worker;

        public override TaskStatus OnUpdate()
        {
            Waypoint.Value = worker.Value.GetRandomWaypoint();

            return TaskStatus.Success;
        }
    }
}
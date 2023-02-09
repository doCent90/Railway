using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Source.Characters.Behaviour.Actions
{
    public class GoToWaitPoint : SetDestination
    {
        public SharedVector3 Waypoint;

        protected override Vector3 GetPosition() => Waypoint.Value;
    }
}
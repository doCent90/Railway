using System;
using BehaviorDesigner.Runtime;
using Source.Characters.Worker.View;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedAnimationEvents : SharedVariable<AnimationEvents>
    {
        public static implicit operator SharedAnimationEvents(AnimationEvents value) =>
            new SharedAnimationEvents {Value = value};
    }
}
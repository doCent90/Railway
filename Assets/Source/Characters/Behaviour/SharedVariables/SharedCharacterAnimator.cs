using System;
using BehaviorDesigner.Runtime;
using Source.Characters.Worker.View;
using Source.Stack;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedCharacterAnimator : SharedVariable<CharacterAnimator>
    {
        public static implicit operator SharedCharacterAnimator(CharacterAnimator value) =>
            new SharedCharacterAnimator {Value = value};
    }
}
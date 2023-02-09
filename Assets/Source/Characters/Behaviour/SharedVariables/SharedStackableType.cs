using System;
using BehaviorDesigner.Runtime;
using Source.Stack;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedStackableType : SharedVariable<StackableType>
    {
        public static implicit operator SharedStackableType(StackableType value) =>
            new SharedStackableType {Value = value};
    }
}
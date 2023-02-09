using System;
using BehaviorDesigner.Runtime;
using Source.Stack;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedStackPresenter : SharedVariable<StackPresenter>
    {
        public static implicit operator SharedStackPresenter(StackPresenter value) =>
            new SharedStackPresenter {Value = value};
    }
}
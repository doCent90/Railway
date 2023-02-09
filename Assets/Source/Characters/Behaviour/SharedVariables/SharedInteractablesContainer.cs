using System;
using BehaviorDesigner.Runtime;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedInteractablesContainer : SharedVariable<InteractableObjectsContainer>
    {
        public static implicit operator SharedInteractablesContainer(InteractableObjectsContainer value) =>
            new SharedInteractablesContainer {Value = value};
    }
}
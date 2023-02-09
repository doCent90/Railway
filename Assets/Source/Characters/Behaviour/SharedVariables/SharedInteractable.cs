using System;
using BehaviorDesigner.Runtime;
using Source.Characters.Behaviour.Interactable;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedInteractable : SharedVariable<CharacterInteractableReference>
    {
        public static implicit operator SharedInteractable(CharacterInteractableReference value) =>
            new SharedInteractable {Value = value};
    }
}
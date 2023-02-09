using System;

namespace Source.Characters.Behaviour.Interactable
{
    [Serializable]
    public class CharacterInteractableReference
    {
        public ICharacterInteractable CharacterInteractable { get; private set; }

        public CharacterInteractableReference(ICharacterInteractable characterInteractable) => 
            CharacterInteractable = characterInteractable ?? throw new ArgumentException();

        public void Reset() => 
            CharacterInteractable = new NullInteractable();
    }
}
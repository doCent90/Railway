using Source.Characters.Behaviour.Interactable;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    internal abstract class InteractableProvider : MonoBehaviour
    {
        public abstract bool TryGetInteractable(StackPresenter stack, out ICharacterInteractable characterInteractable);
    }
}
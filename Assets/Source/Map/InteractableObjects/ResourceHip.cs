using Source.Characters.Behaviour.Interactable;
using Source.InputHandler;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    public class ResourceHip : MonoBehaviour, IClickable
    {
        [SerializeField] private StackPresenter _stack;
        [SerializeField] private InteractableProvider _interactableProvider;

        public int Count => _stack.Count;

        public void Click()
        {
            if(_interactableProvider.TryGetInteractable(_stack, out ICharacterInteractable interactable) == false)
                return;

            if (interactable.CanInteract(_stack))
                interactable.Interact(_stack, 1f);

            if(_stack.Count == 0)
                gameObject.SetActive(false);
        }
    }
}

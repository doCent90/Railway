using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.Interactable;
using Source.Characters.Behaviour.SharedVariables;
using Source.Characters.Worker.View;
using UnityEngine;
using Action = BehaviorDesigner.Runtime.Tasks.Action;

namespace Source.Characters.Behaviour.Actions
{
    public class Interaction : Action
    {
        public SharedInteractable CurrentInteractable;
        public SharedStackPresenter SelfStackPresenter;
        public SharedWorker SharedWorker;
        public SharedCharacterAnimator CharacterAnimator;
        public SharedAnimationEvents AnimationEvents;
        public SharedInteractablesContainer SharedInteractableContainer;

        private WorkerInteractionAnimator _animator;
        private ICharacterInteractable _interactable;

        public override void OnAwake()
        {
            _animator = new WorkerInteractionAnimator(CharacterAnimator.Value, SharedWorker.Value.Stats);
            AnimationEvents.Value.AddListener(_animator);
        }

        public override TaskStatus OnUpdate()
        {
            _interactable = CurrentInteractable.Value.CharacterInteractable;

            if (_interactable.CanInteract(SelfStackPresenter.Value) == false)
            {
                ReleaseInteractable();

                return TaskStatus.Success;
            }

            Interact();

            return TaskStatus.Running;
        }

        private void Interact()
        {
            _animator.DisplayInteraction(_interactable, SelfStackPresenter.Value);
            float interactionDelta = Time.deltaTime * SharedWorker.Value.Stats.InteractionSpeed;
            _interactable.Interact(SelfStackPresenter.Value, interactionDelta);
        }

        public override void OnConditionalAbort() => 
            ReleaseInteractable();

        private void ReleaseInteractable()
        {
            SharedInteractableContainer?.Value?.RemoveActor(SelfStackPresenter.Value);
            CurrentInteractable.Value.Reset();
            _interactable = null;
        }
    }
}
using System;
using Source.Characters.Behaviour;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;
using UnityEngine;

namespace Source.Characters.Worker.View
{
    public class WorkerInteractionAnimator
    {
        private readonly CharacterAnimator _characterAnimator;
        private readonly WorkerStats _workerStats;
        private IStackableResource _lastInteractable;

        public WorkerInteractionAnimator(CharacterAnimator characterAnimator, WorkerStats workerStats)
        {
            _workerStats = workerStats ? workerStats : throw new ArgumentException();
            _characterAnimator = characterAnimator ? characterAnimator : throw new ArgumentException();
        }

        public void DisplayInteraction(ICharacterInteractable interactable, StackPresenter stackPresenter)
        {
            if (interactable is IInteractablesList interactablesList)
                interactable = interactablesList.FirstInteractable(stackPresenter);

            switch (interactable)
            {
                case IWorkSource:
                    _characterAnimator.Work(true);
                    break;
                case IStackableResource stackableResource:
                {
                    _lastInteractable = stackableResource;

                    if (stackableResource.WaitAddToStack == false)
                        Chop(stackableResource.StackableType);

                    break;
                }
            }
        }

        public void Hit() =>
            _lastInteractable?.Hit();

        private void Chop(StackableType type)
        {
            switch (type)
            {
                case StackableType.Wood:
                    _characterAnimator.Chop(true, _workerStats.InteractionSpeed * _workerStats.ChopSpeed);
                    break;
                default:
                    _characterAnimator.ChopPickaxe(true, _workerStats.InteractionSpeed * _workerStats.ChopSpeed);
                    break;
            }
        }
    }
}
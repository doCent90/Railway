using System;
using System.Collections;
using DG.Tweening;
using Source.Characters.Behaviour;
using Source.Characters.Behaviour.Interactable;
using Source.Characters.Worker.View;
using Source.Stack;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Map.InteractableObjects
{
    public class Resource : MonoBehaviour, ICharacterInteractable, IStackableResource
    {
        [SerializeField] private MonoBehaviour _stackableFactoryBehaviour;
        [SerializeField] private ActionProgress _actionProgress;
        [SerializeField] private int _woodPerTree;
        [SerializeField] private LocationView locationView;

        private ISingleTypeStackableFactory SingleTypeStackableFactory => (ISingleTypeStackableFactory) _stackableFactoryBehaviour;
        private int _producedWood;
        private bool _waitAddToStack;

        public StackableType StackableType => SingleTypeStackableFactory.Type;
        public bool WaitAddToStack => _waitAddToStack;

        public void Construct(LocationType location) =>
            locationView.SetLocation(location);

        public bool CanInteract(StackPresenter enteredStack) =>
            enteredStack.CanAddToStack(StackableType) && _actionProgress.Completed == false || _waitAddToStack;

        public bool CanInteract(StackableType stackableType) =>
            StackableType == stackableType;

        public Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition) =>
            transform.position + (transformPosition.position - transform.position).normalized * 1.5f;

        public void Interact(StackPresenter enteredStack, float deltaTime)
        {
            if (CanInteract(enteredStack) == false)
                throw new InvalidOperationException();

            if (_actionProgress.Completed)
                return;

            _actionProgress.AddProgress(deltaTime);
            ProcessWoodProduction(enteredStack);
        }

        public void Hit() =>
            locationView.Hit();

        private void ProcessWoodProduction(StackPresenter enteredStack)
        {
            int currentAmount = (int) (_woodPerTree * _actionProgress.NormalizedProgress);

            if (!ShouldProduceWood(currentAmount))
                return;

            StartCoroutine(ProduceWood(enteredStack, currentAmount));
            _producedWood = currentAmount;
        }

        private bool ShouldProduceWood(int currentAmount) =>
            currentAmount != _producedWood;

        private IEnumerator ProduceWood(StackPresenter enteredStack, int currentAmount)
        {
            foreach (Stackable stackable in enteredStack.RemoveAll())
                Destroy(stackable.gameObject);

            for (int i = 0; i < (currentAmount - _producedWood) * enteredStack.Multiplier; i++)
            {
                Stackable stackable = SingleTypeStackableFactory.Create();
                Vector3 randomAdd = Random.insideUnitSphere * 2f;
                randomAdd.y = 0.25f;
                _waitAddToStack = true;

                stackable.transform.DOJump(transform.position + randomAdd, 1f, 1, 0.5f)
                    .OnComplete(() => AddToStack(enteredStack, stackable));

            }
            locationView.ViewComplete();
            yield return null;
        }

        private void AddToStack(StackPresenter enteredStack, Stackable stackable)
        {
            if (enteredStack != null && enteredStack.CanAddToStack(StackableType))
                enteredStack.AddToStack(stackable);
            else
                Destroy(stackable.gameObject);

            _waitAddToStack = false;
        }
    }
}

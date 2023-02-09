using Source.Map.ChunksLoader;
using Source.Map.InteractableObjects.Payment;
using Source.Map.InteractableObjects.SlicedObjects;
using Source.UI;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Map.InteractableObjects.Cells
{
    class StoneCell : BuildableCell, ICellLock, ITutorial
    {
        [SerializeField] private SlicedObjectPresenter _slicedObjectPresenter;
        [SerializeField] private NavMeshObstacle _navMeshObstacle;
        [SerializeField] private ClickableTutorial _tutorial;
        [SerializeField] private PaymentForAction _paymentForAction;

        public bool Locked => Progress.Completed == false;

        private void OnEnable()
        {
            _slicedObjectPresenter.RockDestroyed += OnRockDestroyed;
        }

        private void OnDisable()
        {
            _slicedObjectPresenter.RockDestroyed -= OnRockDestroyed;
        }

        private void OnRockDestroyed()
        {
            Progress.Complete();
            _navMeshObstacle.enabled = false;

            if (_tutorial != null)
                _tutorial.Click();

            _paymentForAction.Pay();
        }

        public bool Completed => Progress.Completed;

        public void Stop()
        {
        }

        public void StartTutorial()
        {
        }

        public void Finish()
        {
        }
    }
}

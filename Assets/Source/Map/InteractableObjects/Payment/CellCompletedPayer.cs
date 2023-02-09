using Source.Map.InteractableObjects.Cells;
using UnityEngine;

namespace Source.Map.InteractableObjects.Payment
{
    public class CellCompletedPayer : MonoBehaviour
    {
        [SerializeField] private PaymentForAction _paymentForAction;
        [SerializeField] private BuildableCell _cell;

        private void Update()
        {
            if (_cell.Progress.Completed == false)
                return;

            _paymentForAction.Pay();
            enabled = false;
        }
    }
}

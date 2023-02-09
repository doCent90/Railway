using System;
using Source.Characters.Upgrades;
using Source.Money;
using UnityEngine;

namespace Source.Characters.Train.Payment
{
    public class DistancePayer
    {
        private readonly UpgradeValue<float> _incomeUpgradeValue;
        private readonly IEnvironmentPayer _environmentPayer;
        private readonly Transform _transform;

        private float _lastPayed = float.MinValue;
        private readonly IPayAmountProvider _payAmountProvider;

        public DistancePayer(Transform transform, UpgradeValue<float> progression, IEnvironmentPayer payer,
            IPayAmountProvider payAmountProvider)
        {
            _incomeUpgradeValue = progression ?? throw new ArgumentException();
            _environmentPayer = payer ?? throw new ArgumentException();
            _transform = transform ? transform : throw new ArgumentException();
            _payAmountProvider = payAmountProvider ?? throw new ArgumentNullException();
        }

        public void Update() =>
            Pay();

        private void Pay()
        {
            if (_transform.position.x < _lastPayed + 1)
                return;

            float payAmount = _payAmountProvider.GetPayAmount() * _incomeUpgradeValue.Value;

            if (payAmount <= 0)
                return;

            Vector3 position = _transform.position;
            _lastPayed = position.x;

            _environmentPayer.Pay((int)payAmount, position);
        }
    }
}

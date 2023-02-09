using System;

namespace Source.Characters.Train.Payment
{
    public class FixedPayAmountProvider : IPayAmountProvider
    {
        private readonly int _fixedPayAmount = 18;

        public FixedPayAmountProvider(int payAmount)
        {
            if (payAmount < 0)
                throw new ArgumentNullException();

            _fixedPayAmount = payAmount;
        }

        public float GetPayAmount() => _fixedPayAmount;
    }
}

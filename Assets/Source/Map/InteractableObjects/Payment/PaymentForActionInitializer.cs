using System;
using Source.Map.ChunksLoader;
using Source.Money;

namespace Source.Map.InteractableObjects.Payment
{
    public class PaymentForActionInitializer : IChunkFactory
    {
        private readonly IEnvironmentPayer _payer;

        public PaymentForActionInitializer(IEnvironmentPayer payer)
        {
            _payer = payer ?? throw new ArgumentNullException();
        }

        public MapChunk Create(MapChunk mapChunk)
        {
            PaymentForAction paymentForAction = mapChunk.GetComponentInChildren<PaymentForAction>(true);

            if (paymentForAction != null)
                paymentForAction.Construct(_payer);

            return mapChunk;
        }

        public void Destroy(MapChunk chunk)
        {
        }
    }
}

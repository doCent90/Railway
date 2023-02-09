using System;
using System.Collections;
using Source.Money;
using UnityEngine;

namespace Source.Map.InteractableObjects.Payment
{
    public class PaymentForAction : MonoBehaviour
    {
        [SerializeField] private int _amount;
        [SerializeField] private ParticleSystem _particles;
        [SerializeField] private float _paySecondsDelay;

        private IEnvironmentPayer _environmentPayer;

        public void Construct(IEnvironmentPayer environmentPayer)
        {
            _environmentPayer = environmentPayer ?? throw new ArgumentNullException();
        }

        public void Pay()
        {
            StartCoroutine(DelayPay());
        }

        private IEnumerator DelayPay()
        {
            yield return new WaitForSeconds(_paySecondsDelay);
            _environmentPayer.Pay(_amount, transform.position);
            _particles.Play();
        }
    }
}

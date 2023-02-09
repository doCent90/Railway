using System;
using Source.Characters.Upgrades;
using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainCompleteMovement : TrainMovement
    {
        private UpgradeValue<float> _speedValue;
        private int _length;

        public bool FinishedMovement { get; private set; }

        public void Construct(UpgradeValue<float> upgradeValue, int length)
        {
            _length = length;
            _speedValue = upgradeValue ?? throw new ArgumentNullException();
        }

        private void Update()
        {
            FinishedMovement = _length < transform.position.x;

            if (!FinishedMovement)
                Drive();
        }

        private void Drive()
        {
            CurrentSpeed = _speedValue.Value;
            Vector3 previousPosition = transform.position;
            transform.Translate(Vector3.right * CurrentSpeed * Time.deltaTime);
            DeltaMovement = (transform.position - previousPosition).x;
        }
    }
}

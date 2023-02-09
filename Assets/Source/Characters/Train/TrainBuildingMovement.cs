using System;
using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainBuildingMovement : TrainMovement
    {
        private IMovablePath _movablePath;
        public bool Finished => IsFinished();

        public void Construct(IMovablePath movablePath)
        {
            _movablePath = movablePath ?? throw new ArgumentException();
        }

        private void Update()
        {
            if (_movablePath.CanMove(transform.position))
                return;

            Drive(_movablePath.StopPoint);
        }

        private bool IsFinished() =>
            _movablePath.Finished(transform.position);

        private void Drive(Vector3 stopPoint)
        {
            CurrentSpeed = Mathf.MoveTowards(CurrentSpeed, TargetSpeed, Time.deltaTime * SpeedChangePerSecond);
            Vector3 previousPosition = transform.position;
            TargetSpeed = Mathf.Clamp(Vector3.Distance(transform.position, stopPoint) * Speed, 0, float.MaxValue);
            transform.position = Vector3.MoveTowards(transform.position, stopPoint, CurrentSpeed * Time.deltaTime);
            DeltaMovement = (transform.position - previousPosition).x;
        }
    }
}

using System;
using Source.SaveLoad;
using UnityEngine;

namespace Source.Characters
{
    public class DistanceSaver
    {
        private readonly DistanceTraveled _distanceTraveled;
        private readonly Transform _trainMovement;

        public DistanceSaver(DistanceTraveled distanceTraveled, Transform trainMovement)
        {
            _trainMovement = trainMovement ? trainMovement : throw new ArgumentException();
            _distanceTraveled = distanceTraveled ?? throw new ArgumentException();
        }

        public void Save() =>
            _distanceTraveled.Distance = _trainMovement.position.x < 0 ? 0 : _trainMovement.position.x;
    }
}

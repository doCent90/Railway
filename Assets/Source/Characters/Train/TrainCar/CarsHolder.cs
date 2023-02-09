using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Source.Map.ChunksLoader.ObjectsGeneration;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Source.Characters.Train.TrainCar
{
    public class CarsHolder
    {
        private const float JumpPower = 1;
        private const float JumpDuration = 0.2f;
        private const float Offset = 3f;

        private readonly Transform _trainTransform;
        private readonly IObjectsContainer<TrainCar> _carsContainer;

        public CarsHolder(Transform trainTransform, IObjectsContainer<TrainCar> objectsContainer)
        {
            _trainTransform = trainTransform ? trainTransform : throw new ArgumentNullException();
            _carsContainer = objectsContainer ?? throw new ArgumentNullException();
        }

        public IEnumerable<TrainCar> Cars => _carsContainer.Objects;
        public int Count => _carsContainer.Count;

        public void Add(TrainCar trainCar)
        {
            trainCar.transform.SetParent(_trainTransform);

            if (Count == 0)
            {
                trainCar.transform.localPosition = Vector3.left * trainCar.Width / 2f;
            }
            else
            {
                TrainCar last = _carsContainer.Objects.Last();
                Vector3 lastBorder = last.transform.localPosition + Vector3.left * last.Width / 2f;
                trainCar.transform.localPosition = lastBorder + Vector3.left * trainCar.Width / 2f;
            }

            trainCar.transform.DOLocalJump(trainCar.transform.localPosition, JumpPower, 1, JumpDuration);
            _carsContainer.Add(trainCar);
        }

        public void Remove(TrainCar trainCar) =>
            _carsContainer.Remove(trainCar);

        public void MoveAllBefore(TrainCar first, float amount, float moveDuration)
        {
            bool move = false;

            foreach (TrainCar passengerCar in _carsContainer.Objects)
            {
                if (first == passengerCar)
                {
                    move = true;
                }

                if (!move)
                    continue;

                Move(passengerCar, amount, moveDuration);
            }
        }

        public void Move(TrainCar car, float amount, float moveDuration)
        {
            car.transform.DOLocalMove(
                car.transform.localPosition + Vector3.right * amount, moveDuration);
        }

        public void Replace(TrainCar car, TrainCar newCar)
        {
            newCar.transform.SetParent(_trainTransform);
            newCar.transform.localPosition = car.transform.localPosition;
            _carsContainer.Replace(car, newCar);
        }

        public void AnimateSort(float duration)
        {
            List<TrainCar> cars = _carsContainer.Objects.ToList();

            Vector3 posittion = Vector3.zero;

            for (int i = 0; i < cars.Count; i++)
            {
                TrainCar car = cars[i];
                Vector3 offset = Vector3.left * car.Width / 2f;
                posittion += offset;
                car.transform.DOLocalMove(posittion, duration);
                posittion += offset;
            }
        }
    }
}

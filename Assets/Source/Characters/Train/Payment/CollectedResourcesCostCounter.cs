using System;
using System.Linq;
using DG.Tweening;
using Source.Characters.Train.TrainCar;
using Source.Characters.Upgrades;
using Source.Money;
using Source.Stack;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Characters.Train.Payment
{
    public class CollectedResourcesSell
    {
        private readonly IEnvironmentPayer _environmentPayer;
        private readonly UpgradeValue<float> _cost;
        private readonly CarsHolder _carsHolder;

        public CollectedResourcesSell(CarsHolder carsHolder, UpgradeValue<float> cost,
            IEnvironmentPayer environmentPayer)
        {
            _cost = cost ?? throw new ArgumentNullException();
            _carsHolder = carsHolder ?? throw new ArgumentNullException();
            _environmentPayer = environmentPayer ?? throw new ArgumentNullException();
        }

        public float NormalizedProgress => (float)GetAccumulatedResourceCount() / GetCapacity();

        public int GetMaxCostCapacity() => GetCapacity() * (int)_cost.Value;

        private int GetCapacity() => _carsHolder.Cars.Sum(car => car.Capacity);

        public float GetTotalCost()
        {
            int amount = GetAccumulatedResourceCount();

            return amount * _cost.Value;
        }

        private int GetAccumulatedResourceCount() => _carsHolder.Cars.Sum(car => car.ResourceCount);

        public void Sell()
        {
            int cost = (int)_cost.Value;
            cost = cost == 0 ? 1 : cost;

            foreach (TrainCar.TrainCar car in _carsHolder.Cars)
            {
                int count = car.ResourceCount;

                if (count != 0)
                    _environmentPayer.Pay(count * cost, car.transform.position);

                foreach (Stackable stackable in car.RemoveAll())
                {
                    stackable.transform.DOScale(Vector3.zero, 0.5f)
                        .OnComplete(() => Object.Destroy(stackable.gameObject));
                }
            }
        }
    }
}

using System;
using Source.Characters.Train.TrainCar;

namespace Source.Characters.Upgrades
{
    public class CarsAmountUpgradeLevelCondition : ICanBuyCondition
    {
        private readonly CarsHolder _carsHolder;
        private readonly int _maxCount;

        public CarsAmountUpgradeLevelCondition(CarsHolder carsHolder, int current)
        {
            _carsHolder = carsHolder ?? throw new ArgumentNullException();
            _maxCount = current;
        }

        public bool CanBuy() => _maxCount >= _carsHolder.Count + 1;
    }
}

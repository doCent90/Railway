using System;
using System.Linq;
using Source.Characters.Train.TrainCar;

namespace Source.Characters.Train.Payment
{
    internal class PaymentForResources : IPayAmountProvider
    {
        private readonly CarsHolder _carsHolder;

        public PaymentForResources(CarsHolder carsHolder)
        {
            _carsHolder = carsHolder ?? throw new ArgumentNullException();
        }

        public float GetPayAmount() => _carsHolder.Cars.Sum(car => car.ResourceCount);
    }
}

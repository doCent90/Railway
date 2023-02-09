using System;
using System.Linq;
using Source.GlobalMap;
using Source.Map.InteractableObjects;

namespace Source.Characters.Train.TrainCar
{
    internal class TrainLoader
    {
        private readonly RandomStackableFactory _factory;
        private readonly CarsHolder _carsHolder;
        private OfflineIncomeHelper _offlineIncomeHelper;

        public TrainLoader(CarsHolder carsHolder, RandomStackableFactory factory,
            OfflineIncomeHelper offlineIncomeHelper)
        {
            _offlineIncomeHelper = offlineIncomeHelper ?? throw new ArgumentNullException();
            _factory = factory ? factory : throw new ArgumentNullException();
            _carsHolder = carsHolder ?? throw new ArgumentNullException();
        }

        public void Load()
        {
            TrainCar[] carsHolderCars = _carsHolder.Cars.ToArray();

            if (carsHolderCars.Length == 0)
                throw new InvalidOperationException();

            float progress = _offlineIncomeHelper.GetNormalizedProgress();
            int total = carsHolderCars.Sum(car => car.Capacity);
            int shippingAmount = (int)(total * progress);

            foreach (TrainCar passengerCar in carsHolderCars)
            {
                while (passengerCar.CanAddAmount > 0 && shippingAmount > 0)
                {
                    passengerCar.Add(_factory.Create());
                    shippingAmount--;
                }
            }
        }
    }
}

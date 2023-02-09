using System;
using System.Linq;
using Source.Characters.Behaviour;
using Source.Characters.Worker.Merge.GenericMerge;

namespace Source.Characters.Train.TrainCar
{
    internal class TrainCarFactory : IMergeableFactory<TrainCarStats, TrainCar>
    {
        private readonly InteractableObjectsContainer _container;
        private readonly TrainCarList _trainCarList;
        private readonly CarsHolder _carsHolder;

        public TrainCarFactory(TrainCarList trainCarList, CarsHolder carsHolder,
            InteractableObjectsContainer container)
        {
            _container = container ?? throw new ArgumentNullException();
            _carsHolder = carsHolder ?? throw new ArgumentNullException();
            _trainCarList = trainCarList ? trainCarList : throw new ArgumentNullException();
        }

        public TrainCar Create(TrainCarStats stats)
        {
            TrainCar template = _trainCarList.TrainCarConfigs.First(config => config.CarStats == stats).CarTemplate;
            TrainCar car = UnityEngine.Object.Instantiate(template);

            _container.Add(car.Interactable, 1);
            _carsHolder.Add(car);

            return car;
        }
    }
}

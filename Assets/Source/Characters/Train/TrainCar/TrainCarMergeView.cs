using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Source.Characters.Behaviour;
using Source.Characters.Worker.Merge.GenericMerge;
using Source.Common;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Characters.Train.TrainCar
{
    internal class TrainCarMergeView : IMergeView<TrainCar>
    {
        private readonly InteractableObjectsContainer _container;
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly CarsHolder _carsHolder;
        private Coroutine _mergeCoroutine;
        private float _mergeDuration = 0.3f;

        public TrainCarMergeView(CarsHolder carsHolder, InteractableObjectsContainer container,
            ICoroutineRunner coroutineRunner)
        {
            _container = container ?? throw new ArgumentNullException();
            _coroutineRunner = coroutineRunner ?? throw new ArgumentNullException();
            _carsHolder = carsHolder ?? throw new ArgumentNullException();
        }

        public void Merge(IEnumerable<TrainCar> cars, TrainCar newCar)
        {
            newCar.gameObject.SetActive(false);
            TrainCar[] passengerCars = cars as TrainCar[] ?? cars.ToArray();

            _mergeCoroutine =
                _coroutineRunner.StartCoroutine(AnimateMerge(passengerCars, newCar, _mergeCoroutine));
        }

        private IEnumerator AnimateMerge(TrainCar[] cars, TrainCar newCar, Coroutine coroutine)
        {
            if (coroutine != null)
                yield return coroutine;

            _carsHolder.Remove(newCar);
            Vector3 transformLocalPosition = cars[1].transform.localPosition;
            cars[0].transform.DOLocalMove(transformLocalPosition, _mergeDuration);
            cars[2].transform.DOLocalMove(transformLocalPosition, _mergeDuration);

            foreach (TrainCar passengerCar in cars)
                _container.Remove(passengerCar.Interactable);

            newCar.gameObject.SetActive(false);

            yield return new WaitForSeconds(_mergeDuration);

            newCar.gameObject.SetActive(true);
            newCar.transform.DOPunchScale(Vector3.one * 0.1f, 0.3f);

            _carsHolder.Remove(cars[0]);
            _carsHolder.Remove(cars[2]);
            _carsHolder.Replace(cars[1], newCar);

            _carsHolder.AnimateSort(1);

            foreach (TrainCar passengerCar1 in cars)
            {
                newCar.Merge(passengerCar1);
                _carsHolder.Remove(passengerCar1);
                Object.Destroy(passengerCar1.gameObject);
            }

            yield return new WaitForSeconds(1);
        }
    }
}

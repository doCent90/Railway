using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainCompleted : Train
    {
        [SerializeField] private TrainCompleteMovement _trainMovement;
        [SerializeField] private Transform _carsRoot;

        public TrainCompleteMovement TrainMovement => _trainMovement;
        public Transform CarsRoot => _carsRoot;
    }
}

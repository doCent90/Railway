using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainStarted : Train
    {
        [SerializeField] private TrainBuildingMovement _trainMovement;

        public TrainBuildingMovement TrainMovement => _trainMovement;
    }
}

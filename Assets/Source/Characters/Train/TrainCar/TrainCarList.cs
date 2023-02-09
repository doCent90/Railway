using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Characters.Train.TrainCar
{
    [CreateAssetMenu(menuName = "Create TrainCarList", fileName = "TrainCar", order = 0)]
    public class TrainCarList : ScriptableObject
    {
        [SerializeField] private List<TrainCarConfig> _trainCarConfigs;

        public IEnumerable<TrainCarConfig> TrainCarConfigs => _trainCarConfigs;
        public IEnumerable<TrainCarStats> TrainCarStats => _trainCarConfigs.Select(config => config.CarStats);
    }

    [Serializable]
    public class TrainCarConfig
    {
        [field: SerializeField] public TrainCar CarTemplate { get; private set; }
        [field: SerializeField] public TrainCarStats CarStats { get; private set; }
    }

    [Serializable]
    public class TrainCarStats
    {
    }
}

using UnityEngine;

namespace Source.Characters.Upgrades
{
    [CreateAssetMenu(fileName = "TrainSpeedProgression", menuName = "Create TrainSpeedProgression", order = 0)]
    public class TrainSpeedProgression : ScriptableObject
    {
        [SerializeField] private LevelFloatProgression _trainSpeedProgression;

        public LevelFloatProgression SpeedProgression => _trainSpeedProgression;
    }
}

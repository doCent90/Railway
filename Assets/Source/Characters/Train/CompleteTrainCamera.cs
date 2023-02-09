using Cinemachine;
using UnityEngine;

namespace Source.Characters.Train
{
    internal class CompleteTrainCamera : MonoBehaviour
    {
        [SerializeField] private CinemachineTargetGroup _cinemachineTargetGroup;

        public CinemachineTargetGroup TargetGroup => _cinemachineTargetGroup;
    }
}

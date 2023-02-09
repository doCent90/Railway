using UnityEngine;

namespace Source.Characters.Train
{
    public abstract class Train : MonoBehaviour
    {
        [SerializeField] private Transform[] _wayPoints;
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private ParticleSystem _mergeParticles;
        [SerializeField] private StackFulfillInteractable _stackFulfillInteractable;

        public Transform[] WayPoints => _wayPoints;
        public Transform SpawnPoint => _spawnPoint;
        public ParticleSystem MergeParticles => _mergeParticles;
        public StackFulfillInteractable StackFulfillInteractable => _stackFulfillInteractable;
    }
}

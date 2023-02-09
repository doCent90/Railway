using System;
using Source.Characters.Worker.Movement;
using UnityEngine;

namespace Source.Characters.Worker
{
    internal class CharacterMovementSpeedUpdater : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;
        [SerializeField] private ParticleSystem _particleSystem;

        private WorkerStats _stats;

        public void Construct(WorkerStats stats) =>
            _stats = stats ? stats : throw new ArgumentException();

        private void Update()
        {
            if (_characterMovement.Speed < _stats.Speed)
            {
                _characterMovement.SetSpeed(_stats.Speed);
                _particleSystem.Play();
            }
        }
    }
}
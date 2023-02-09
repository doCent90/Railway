using System.Collections.Generic;
using UnityEngine;

namespace Source.Characters.Train
{
    public class HandCarAnimator : MonoBehaviour
    {
        [SerializeField] private List<Animator> _animator;
        [SerializeField] private TrainMovement _trainMovement;
        [SerializeField] private float _animationSpeed;

        private void Update()
        {
            foreach (Animator animator in _animator)
                animator.speed = _trainMovement.DeltaMovement * _animationSpeed / Time.deltaTime;
        }
    }
}

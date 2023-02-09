using System.Collections.Generic;
using UnityEngine;

namespace Source.Characters.Worker.View
{
    public class AnimationEvents : MonoBehaviour
    {
        private readonly List<WorkerInteractionAnimator> _animators = new();

        public void Hit()
        {
            foreach (WorkerInteractionAnimator animator in _animators) 
                animator.Hit();
        }

        public void AddListener(WorkerInteractionAnimator animator) => 
            _animators.Add(animator);
    }
}

using Source.Characters.Worker.Movement;
using UnityEngine;

namespace Source.Characters.Worker.View
{
    [RequireComponent(typeof(CharacterAnimator))]
    public class CharacterSpeedAnimation : MonoBehaviour
    {
        [SerializeField] private CharacterMovement _characterMovement;
        
        private CharacterAnimator _characterAnimator;

        private void Awake() => 
            _characterAnimator = GetComponent<CharacterAnimator>();

        private void Start() => 
            _characterAnimator.SetSpeed(0);

        private void LateUpdate() => 
            _characterAnimator.SetSpeed(_characterMovement.DeltaMovement);
    }
}
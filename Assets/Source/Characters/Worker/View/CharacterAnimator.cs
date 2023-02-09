using UnityEngine;

namespace Source.Characters.Worker.View
{
    public class CharacterAnimator : MonoBehaviour
    {
        private static readonly int ChopAnimation = Animator.StringToHash("Chop");
        private static readonly int ChopPickaxeAnimation = Animator.StringToHash("ChopPickaxe");
        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int InteractionSpeed = Animator.StringToHash("InteractionSpeed");
        private static readonly int WorkTrigger = Animator.StringToHash("Work");

        [SerializeField] private Animator _animator;

        private void LateUpdate()
        {
            Chop(false, 1);
            ChopPickaxe(false, 1);
            Work(false);
        }

        public void SetSpeed(float normalizedSpeed) =>
            _animator.SetFloat(Speed, normalizedSpeed);

        public void Chop(bool value, float interactionSpeed)
        {
            _animator.SetFloat(InteractionSpeed, interactionSpeed);
            _animator.SetBool(ChopAnimation, value);
        }

        public void ChopPickaxe(bool value, float interactionSpeed)
        {
            _animator.SetFloat(InteractionSpeed, interactionSpeed);
            _animator.SetBool(ChopPickaxeAnimation, value);
        }

        public void Work(bool value) => 
            _animator.SetBool(WorkTrigger, value);
    }
}
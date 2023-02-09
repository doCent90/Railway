using UnityEngine;

namespace Source.Map.InteractableObjects.SlicedObjects
{
    public class SlicedPartAnimator
    {
        private const float ExplosionRaius = 10f;
        private const float ExplosionForce = 300f;

        public void Move(Transform centrPosition, SlicedPart part)
        {
            part.Collider.isTrigger = false;
            part.Rigidbody.isKinematic = false;
            part.Rigidbody.AddExplosionForce(ExplosionForce, centrPosition.position, ExplosionRaius);
            part.Disable();
        }
    }
}

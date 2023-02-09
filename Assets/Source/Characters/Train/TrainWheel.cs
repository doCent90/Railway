using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainWheel : MonoBehaviour
    {
        [SerializeField] private TrainMovement trainMovement;
        [SerializeField] private float _rotationSpeed;

        private void Update() =>
            Rotate();

        private void Rotate()
        {
            if (trainMovement == null)
                return;

            float speed = _rotationSpeed * trainMovement.DeltaMovement;
            transform.rotation = Quaternion.Euler(-transform.right * speed) * transform.rotation;
        }
    }
}

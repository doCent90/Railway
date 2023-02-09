using UnityEngine;

namespace Source.Characters.Train
{
    public abstract class TrainMovement : MonoBehaviour
    {
        [SerializeField] protected float Speed;
        [SerializeField] protected float SpeedChangePerSecond;

        protected float TargetSpeed;
        protected float CurrentSpeed;

        public float CommonSpeed => CurrentSpeed;
        public float DeltaMovement { get; protected set; }
    }
}

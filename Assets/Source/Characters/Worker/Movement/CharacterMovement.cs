using UnityEngine;

namespace Source.Characters.Worker.Movement
{
    [RequireComponent(typeof(NavMeshMovement))]
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private MonoBehaviour _inputSourceBehaviour;

        private NavMeshMovement _movement;
        private IInputSource _inputSource;

        public float DeltaMovement { get; private set; }

        public float Speed => _speed;

        private void Awake()
        {
            _movement = GetComponent<NavMeshMovement>();
            _inputSource = (IInputSource) _inputSourceBehaviour;
        }

        private void Update()
        {
            if (_inputSource.Destination != transform.position)
                Move();
            else
                Stop();
        }

        public void SetSpeed(float speed) =>
            _speed = speed;

        private void Stop()
        {
            if (_movement != null)
                _movement.Move(transform.position);
        }

        private void Move()
        {
            _movement.Move(_inputSource.Destination);

            float distanceToDestination = Vector3.Distance(transform.position, _inputSource.Destination);

            float deltaMovement = Mathf.Clamp01(distanceToDestination);
            DeltaMovement = deltaMovement;
            _movement.SetSpeed(_speed * deltaMovement);
        }
    }
}
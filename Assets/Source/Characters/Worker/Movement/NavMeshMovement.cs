using UnityEngine;
using UnityEngine.AI;

namespace Source.Characters.Worker.Movement
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

#if UNITY_EDITOR
        [SerializeField] private bool _drawGizmos;
#endif

        private NavMeshAgent _agent;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            enabled = false;

            _agent.speed = _speed;
        }

        public void Move(Vector3 target)
        {
            _agent.SetDestination(target);
            enabled = true;
        }

        public void SetSpeed(float speedRate) =>
            _agent.speed = speedRate;

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_drawGizmos == false || _agent == null)
                return;

            Vector3[] navMeshPath = _agent.path.corners;

            Gizmos.color = Color.red;

            for (int i = 0; i < navMeshPath.Length - 1; i++)
                Gizmos.DrawLine(navMeshPath[i], navMeshPath[i + 1]);
        }
#endif
    }
}
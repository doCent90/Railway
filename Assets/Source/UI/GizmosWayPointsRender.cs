#if UNITY_EDITOR
using UnityEngine;

namespace Source.UI
{
    public class GizmosWayPointsRender : MonoBehaviour
    {
        [SerializeField] private Transform[] _wayPoints;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            for (int i = 0; i < _wayPoints.Length; i++)
            {
                if(i < _wayPoints.Length - 1)
                    Gizmos.DrawLine(_wayPoints[i].position, _wayPoints[i + 1].position);
            }
        }
    }
}
#endif

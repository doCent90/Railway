using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.Environment
{
    public class ObjectWithBounds : MonoBehaviour
    {
        [SerializeField] private Color _gizmosColor;
        [SerializeField] private bool _drawGizmos;

        [field: SerializeField] public Bounds Bounds { get; private set; }

        public bool Contains(Vector3 point) =>
            Bounds.Contains(point - transform.position);

        private void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;

            Gizmos.color = _gizmosColor;
            Gizmos.DrawCube(transform.position + Bounds.center, Bounds.extents * 2);
        }
    }
}
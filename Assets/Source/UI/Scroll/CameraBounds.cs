using UnityEngine;

namespace Source.UI
{
    public class CameraBounds : MonoBehaviour
    {
        [SerializeField] private Color _color;

        [field: SerializeField] public float NearZoomLimit { get; private set; } = -13f;
        [field: SerializeField] public float FarZoomLimit { get; private set; } = 4f;
        [field: SerializeField] public float Left { get; private set; }
        [field: SerializeField] public float Right { get; private set; }
        [field: SerializeField] public float Top { get; private set; }
        [field: SerializeField] public float Bottom { get; private set; }

        private void OnDrawGizmos()
        {
            Vector3 leftTop = new Vector3(Left, 2, Top);
            Vector3 leftBottom = new Vector3(Left, 2, Bottom);
            Vector3 rightTop = new Vector3(Right, 2, Top);
            Vector3 rightBottom = new Vector3(Right, 2, Bottom);

            Gizmos.color = _color;
            Gizmos.DrawLine(leftTop, rightTop);
            Gizmos.DrawLine(rightTop, rightBottom);
            Gizmos.DrawLine(rightBottom, leftBottom);
            Gizmos.DrawLine(leftBottom, leftTop);
        }
    }
}

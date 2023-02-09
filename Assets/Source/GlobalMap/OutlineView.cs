using UnityEngine;

namespace Source.GlobalMap
{
    public abstract class OutlineView : MonoBehaviour
    {
        [SerializeField] protected LineRenderer LineRenderer;
        [SerializeField] protected Transform[] Points;

        private void Start() => EnableLine();

        public void EnableLine()
        {
            Vector3[] points = new Vector3[Points.Length];

            for (int i = 0; i < Points.Length; i++)
                points[i] = Points[i].position;

            LineRenderer.positionCount = Points.Length;
            LineRenderer.SetPositions(points);
        }

#if UNITY_EDITOR

        [ContextMenu(nameof(Enable))]
        public void Enable()
        {
            EnableLine();
        }
#endif
    }
}

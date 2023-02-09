using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsProvider
{
    public class DiscSamplingGenerator : MonoBehaviour, IPointsProvider
    {
        [SerializeField] private float _maxRadius = 1;
        [SerializeField] private float _minRadius = 1;
        [SerializeField] private float _center;
        [SerializeField] private float _falloff = 1;
        [SerializeField] private Vector2 _regionSize = Vector2.one;
        [SerializeField] private int _rejectionSamples = 30;
        [SerializeField] private float _displayRadius = 1;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private List<Vector3> points;
        [SerializeField] private bool _drawGizmos;

        public IEnumerable<Vector3> Points => points;

        [ContextMenu("create")]
        void Create()
        {
            points = PoissonDiscSampling.GeneratePoints(_maxRadius, _minRadius, _regionSize, _falloff, _center,  _rejectionSamples)
                .Select(point => new Vector3(point.x, 0, point.y) + _offset).ToList();
        }

        void OnDrawGizmos()
        {
            if (!_drawGizmos)
                return;
            
            Gizmos.DrawWireCube(_regionSize / 2, _regionSize);

            if (points != null)
            {
                foreach (Vector3 point in points)
                    Gizmos.DrawSphere(point, _displayRadius);
            }
        }
    }
}
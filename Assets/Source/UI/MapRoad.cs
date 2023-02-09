using DG.Tweening;
using RunnerMovementSystem;
using UnityEngine;

namespace Source.UI
{
    public class MapRoad : MonoBehaviour
    {
        [SerializeField] private PathSegment _path;
        [SerializeField] private Material _builtMaterial;
        [SerializeField] private Gradient _builtGradient;

        [field: SerializeField] public LineRenderer LineRenderer { get; private set; }
        public PathSegment Path => _path;

        public void Complete()
        {
            LineRenderer.colorGradient = _builtGradient;
            LineRenderer.material = _builtMaterial;
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}

using DG.Tweening;
using UnityEngine;

namespace Source.Stack
{
    public class ClickableWoodAnimate : MonoBehaviour
    {
        private const float Delay = 0.5f;
        private const float EndValue = 3f;
        private const string OutLine = "_OutlineWidth";

        [SerializeField] private MeshRenderer _meshRenderer;

        private Material _material;

        private void Start()
        {
            _material = _meshRenderer.material;
            Animating();
        }

        private void Animating()
        {
            Sequence sequence = DOTween.Sequence();

            sequence.Append(_material.DOFloat(0, OutLine, Delay));
            sequence.Append(_material.DOFloat(EndValue, OutLine, Delay));
            sequence.SetLoops(-1);
        }
    }
}

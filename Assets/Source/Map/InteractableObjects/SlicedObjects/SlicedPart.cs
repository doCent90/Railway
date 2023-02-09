using UnityEngine;
using DG.Tweening;

namespace Source.Map.InteractableObjects.SlicedObjects
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(MeshRenderer))]
    public class SlicedPart : MonoBehaviour
    {
        private const float Delay = 1f;
        private const float Duration = 0.5f;

        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }
        [field: SerializeField] public Collider Collider { get; private set; }
        [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
        public bool HasExpoloded { get; private set; } = false;

#if UNITY_EDITOR
        private void OnValidate()
        {
            Collider = GetComponent<Collider>();
            Rigidbody = GetComponent<Rigidbody>();
            MeshRenderer = GetComponent<MeshRenderer>();
        }
#endif

        public void Disable()
        {
            HasExpoloded = true;
            transform.DOScale(0, Duration).SetDelay(Delay).OnComplete(() => Destroy(gameObject));
        }
    }
}

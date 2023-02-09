using UnityEngine;

namespace Source.GlobalMap
{
    public class Region : MonoBehaviour
    {
        [field: SerializeField] public SpriteRenderer LockedRegion { get; private set; }
        [field: SerializeField] public SpriteRenderer Lock { get; private set; }
        [field: SerializeField] public SpriteRenderer UnLock { get; private set; }
        [field: SerializeField] public Transform LocksContainer { get; private set; }

        public void Open()
        {
            gameObject.SetActive(false);
        }

        public void Close()
        {
            gameObject.SetActive(true);
        }
    }
}

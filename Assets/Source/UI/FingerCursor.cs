using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class FingerCursor : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private Transform _fingerParent;
        [SerializeField] private Image _finger;

        private void OnEnable()
        {
            _finger.enabled = true;
        }

        private void Update()
        {
            _fingerParent.transform.position = Input.mousePosition;
        }
#endif
    }
}

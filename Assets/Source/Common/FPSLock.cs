using UnityEngine;

namespace Source.Common
{
    public class FPSLock : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private int _editorLock;
#endif

        private void Awake()
        {
#if !UNITY_EDITOR
            Application.targetFrameRate = 60;
#else
            if (_editorLock > 0)
                Application.targetFrameRate = _editorLock;
#endif
        }
    }
}
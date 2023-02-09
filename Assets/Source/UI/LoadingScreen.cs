using UnityEngine;
using Source.Extensions;

namespace Source.UI
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;

        public void Enable() => _canvasGroup.EnableGroup();
    }
}

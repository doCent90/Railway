using UnityEngine;

namespace Source.UI
{
    public class GlobalProgressUI : MonoBehaviour
    {
        [field: SerializeField] public GlobalProgressView GlobalProgressView { get; private set; }
        [field: SerializeField] public ButtonsUI ButtonsUI { get; private set; }
    }
}

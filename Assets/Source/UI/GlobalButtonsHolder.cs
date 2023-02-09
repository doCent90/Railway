using System.Collections.Generic;
using UnityEngine;

namespace Source.UI
{
    public class GlobalButtonsHolder : MonoBehaviour
    {
        [SerializeField] private GlobalProgressUI[] _globalProgressUIs;

        public IReadOnlyList<GlobalProgressUI> GlobalProgressUIs => _globalProgressUIs;
    }
}

using UnityEngine;

namespace Source.Map.InteractableObjects.Cells
{
    public class BuildableCell : MonoBehaviour
    {
        [SerializeField] private ActionProgress _progress;

        public ActionProgress Progress => _progress;
    }
}
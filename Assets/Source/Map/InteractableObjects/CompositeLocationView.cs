using System.Linq;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    class CompositeLocationView : MonoBehaviour, ILocationView
    {
        [SerializeField] private MonoBehaviour[] _locationViewsBehaviours;

        private void OnValidate()
        {
            if(_locationViewsBehaviours == null)
                return;

            for (int i = 0; i < _locationViewsBehaviours.Length; i++)
            {
                MonoBehaviour locationViewsBehaviour = _locationViewsBehaviours[i];

                if (locationViewsBehaviour != null && locationViewsBehaviour is ILocationView == false)
                {
                    Debug.LogWarning(nameof(locationViewsBehaviour) + " needs to implement " + nameof(ILocationView));
                    _locationViewsBehaviours[i] = null;
                }
            }
        }

        public void SetLocation(LocationType type)
        {
            foreach (ILocationView locationView in _locationViewsBehaviours.Cast<ILocationView>())
                locationView.SetLocation(type);
        }
    }
}

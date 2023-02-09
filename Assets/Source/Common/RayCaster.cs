using UnityEngine;

namespace Source.Common
{
    public class RayCaster : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }

        public bool TryRaycastComponent<T>(out T component, Vector2 screenPosition) where T : class
        {
            component = null;

            if (TryCastRay(out RaycastHit hit, screenPosition))
            {
                if (hit.transform.TryGetComponent(out T hitComponent))
                {
                    component = hitComponent;
                    return true;
                }
            }

            return false;
        }

        private bool TryCastRay(out RaycastHit hit, Vector2 screenPosition)
        {
            hit = new RaycastHit();
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, Mathf.Infinity))
            {
                hit = raycastHit;
                return true;
            }

            return false;
        }
    }
}

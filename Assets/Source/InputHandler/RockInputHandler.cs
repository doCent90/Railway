using System;
using UnityEngine;
using Source.Map.InteractableObjects.SlicedObjects;

namespace Source.InputHandler
{
    public class RockInputHandler : MonoBehaviour
    {
        private Camera _camera;

        public event Action<SlicedPart> Clicked;

        private void Start() => _camera = Camera.main;

        private void Update()
        {
            if (_camera == null) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit))
                    if (hit.collider.TryGetComponent(out SlicedPart part) && part.HasExpoloded == false)
                        Clicked?.Invoke(part);
            }
        }
    }
}
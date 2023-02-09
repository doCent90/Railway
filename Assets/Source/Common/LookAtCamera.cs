using UnityEngine;

namespace Source.Common
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private bool _update;
        [SerializeField] private bool _lookFromCamera;

        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
            Rotate();
        }

        private void Update()
        {
            if (_update)
                Rotate();
        }

        private void Rotate()
        {
            Vector3 position = _camera.transform.position;

            if (_lookFromCamera)
                position = transform.position * 2 - position;

            transform.LookAt(position);
        }
    }
}
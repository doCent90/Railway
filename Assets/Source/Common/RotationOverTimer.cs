using UnityEngine;

namespace Source.Common
{
    public class RotationOverTimer : MonoBehaviour
    {
        [SerializeField] private float _speed;
    
        void Update() => 
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }
}

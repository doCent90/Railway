using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Common
{
    public class RandomRotation : MonoBehaviour
    {
        [SerializeField] private Vector3 _axisMin;
        [SerializeField] private Vector3 _axisMax;

        private void Start()
        {
            Vector3 eulerAngles = new Vector3(Random.Range(_axisMin.x, _axisMax.x), Random.Range(_axisMin.y, _axisMax.y),
                Random.Range(_axisMin.z, _axisMax.z));
        
            transform.eulerAngles = eulerAngles;
        }
    }
}
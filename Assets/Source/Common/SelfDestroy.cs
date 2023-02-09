using System.Collections;
using UnityEngine;

namespace Source.Common
{
    public class SelfDestroy : MonoBehaviour
    {
        [SerializeField] private float _delay = 2f;

        private void Start() =>
            StartCoroutine(Destroy());

        private IEnumerator Destroy()
        {
            yield return new WaitForSeconds(_delay);
            Destroy(gameObject);
        }
    }
}
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Source.Characters.Train
{
    public class TrainAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;

        private void Start() => 
            StartCoroutine(Animate());

        private IEnumerator Animate()
        {
            while (true)
            {
                transform.DOPunchScale(Vector3.up * 0.02f, _duration, 0, 0.1f);
                yield return new WaitForSeconds(_duration);
            }
        }

        private void OnDestroy() =>
            transform.DOComplete();
    }
}
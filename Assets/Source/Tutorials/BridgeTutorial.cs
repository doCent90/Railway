using System;
using System.Collections;
using System.Linq;
using Source.Map.InteractableObjects;
using Source.UI;
using UnityEngine;

namespace Source.Tutorials
{
    public class BridgeTutorial : MonoBehaviour, ITutorial
    {
        [SerializeField] private ResourceHip[] _resourceHips;
        [SerializeField] private ButtonWorldToScreenPosition _pointer;

        private Coroutine _tutorial;
        private int _lastCount;
        private Camera _camera;

        public bool Completed => _resourceHips.All(hip => hip.Count == 0);

        private void Awake()
        {
            _camera = Camera.main;
        }

        public void Stop()
        {
            _pointer.gameObject.SetActive(false);
        }

        public void StartTutorial()
        {
            _tutorial = StartCoroutine(Tutorial());
            enabled = true;
        }

        private IEnumerator Tutorial()
        {
            while (enabled)
            {
                yield return new WaitUntil(() => Mathf.Abs(_camera.WorldToViewportPoint(transform.position).x) < 1);
                _lastCount = GetWoodCount();

                yield return new WaitForSecondsRealtime(4f);

                if (_lastCount != GetWoodCount())
                    continue;

                ResourceHip first = _resourceHips.First(hip => hip.Count > 0);
                _pointer.gameObject.SetActive(true);
                _pointer.SetTargetPosition(first.transform);

                yield return new WaitUntil(() => _lastCount != GetWoodCount());
                _pointer.gameObject.SetActive(false);
            }
        }

        private int GetWoodCount() => _resourceHips.Sum(hip => hip.Count);

        public void Finish()
        {
            Stop();
            enabled = false;
            StopCoroutine(_tutorial);
        }
    }
}

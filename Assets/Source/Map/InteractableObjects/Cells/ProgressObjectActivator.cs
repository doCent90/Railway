using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Source.Map.InteractableObjects.Cells
{
    internal class ProgressObjectActivator : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _objects;
        [SerializeField] private float _duration;
        [SerializeField] private float _jumpPower;

        public void Display(Transform actor, float progress)
        {
            for (int i = 0; i < _objects.Count; i++)
            {
                bool active = (float) (i + 1) / _objects.Count <= progress + 0.001f;

                if (active && !_objects[i].activeSelf)
                    StartCoroutine(Animate(_objects[i].transform, actor));

                _objects[i].SetActive(active);
            }
        }

        private IEnumerator Animate(Transform target, Transform from)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 targetRotation = target.transform.localRotation.eulerAngles;
            target.transform.position = from.transform.position;
            target.DOMove(targetPosition, _duration);
            target.DOLocalRotate(targetRotation, _duration);
            target.transform.DOJump(targetPosition, _jumpPower, 1, _duration);

            yield return null;
        }
    }
}
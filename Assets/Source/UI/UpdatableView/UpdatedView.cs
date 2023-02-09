using System;
using System.Collections;
using UnityEngine;

namespace Source.UI.UpdatableView
{
    internal class UpdatedView : MonoBehaviour
    {
        [SerializeField] private GameObject _updatedIndicator;
        [SerializeField] private Animation _animation;

        private IUpdateCondition _updateCondition;

        public void Construct(IUpdateCondition updateCondition)
        {
            _updateCondition = updateCondition ?? throw new ArgumentNullException();
        }

        private void Update()
        {
            if (!_updateCondition.Updated)
                return;

            _updatedIndicator.gameObject.SetActive(true);
            StartCoroutine(PlayPulsation());
            enabled = false;
        }

        private IEnumerator PlayPulsation()
        {
            yield return new WaitForSecondsRealtime(90f);
            _animation.Play();
        }
    }
}

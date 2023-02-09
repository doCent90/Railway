using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Source.Map.InteractableObjects.SlicedObjects
{
    public class SlicedObjectAnimator
    {
        private const float ScaleFactor = 1.2f;
        private const float ScaleDuration = 0.1f;
        private const int ScaleFactorParticle = 3;

        private readonly SlicedObject _slicedObject;
        private readonly Transform _slicedPartsContainer;
        private readonly ParticleSystem _particleSystem;

        public SlicedObjectAnimator(Transform slicedPartsContainer, SlicedObject slicedObject, ParticleSystem particleSystem)
        {
            _slicedPartsContainer = slicedPartsContainer;
            _slicedObject = slicedObject;
            _particleSystem = particleSystem;

            _slicedObject.Clicked += AnimateParentModel;
            _slicedObject.AllPartsMoved += OnAllPartsMoved;
            _slicedObject.SinglePartMoved += OnSinglePartMoved;
        }

        public void OnDisable()
        {
            _slicedObject.Clicked -= AnimateParentModel;
            _slicedObject.AllPartsMoved -= OnAllPartsMoved;
            _slicedObject.SinglePartMoved -= OnSinglePartMoved;
        }

        private void OnAllPartsMoved()
        {
            _particleSystem.Play();
            _particleSystem.transform.localScale = Vector3.one * ScaleFactorParticle;
        }

        private void OnSinglePartMoved(Transform part)
        {
            _particleSystem.Play();
            _particleSystem.transform.position = part.transform.position + Vector3.left;
        }

        private void AnimateParentModel()
        {
            _slicedPartsContainer.transform.DOScale(ScaleFactor, ScaleDuration);
            _slicedPartsContainer.transform.DOScale(Vector3.one, ScaleDuration).SetDelay(ScaleDuration);
        }
    }
}

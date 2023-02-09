using System.Collections.Generic;
using Source.InputHandler;
using UnityEngine;
using System;

namespace Source.Map.InteractableObjects.SlicedObjects
{
    public class SlicedObjectPresenter : MonoBehaviour
    {
        [SerializeField] private List<SlicedPart> _slicedParts;
        [SerializeField] private RockInputHandler _inputHandler;
        [SerializeField] private Transform _slicedPartsContainer;
        [SerializeField] private ParticleSystem _particleSystem;

        private SlicedObject _slicedObject;
        private SlicedObjectAnimator _slicedObjectAnimator;
        private IReadOnlyList<SlicedPart> _slicedPartsRead => _slicedParts;

        public event Action RockDestroyed;

        private void OnEnable()
        {
            _slicedObject = new SlicedObject(_inputHandler, _slicedPartsRead, _slicedPartsContainer);
            _slicedObjectAnimator = new SlicedObjectAnimator(_slicedPartsContainer, _slicedObject, _particleSystem);

            _slicedObject.Destroyed += DestroyRock;
        }

        private void OnDisable()
        {
            _slicedObject.OnDisable();
            _slicedObjectAnimator.OnDisable();
            _slicedObject.Destroyed -= DestroyRock;
        }

        private void DestroyRock() => RockDestroyed?.Invoke();
    }
}

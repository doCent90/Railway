using System.Collections.Generic;
using Source.InputHandler;
using UnityEngine;
using System;

namespace Source.Map.InteractableObjects.SlicedObjects
{
    public class SlicedObject
    {
        private const int Devider = 10;

        private readonly List<SlicedPart> _slicedParts;
        private readonly Transform _centr;
        private readonly RockInputHandler _inputHandler;
        private readonly SlicedPartAnimator _slicedPartAnimator;
        private int _minCountToDestroy;

        public event Action Clicked;
        public event Action Destroyed;
        public event Action AllPartsMoved;
        public event Action<Transform> SinglePartMoved;

        public SlicedObject(RockInputHandler inputHandler, IReadOnlyList<SlicedPart> slicedParts, Transform centr)
        {
            _centr = centr;
            _inputHandler = inputHandler;
            _slicedParts = (List<SlicedPart>)slicedParts;
            _minCountToDestroy = _slicedParts.Count / Devider;
            _slicedPartAnimator = new SlicedPartAnimator();

            _inputHandler.Clicked += OnClicked;
        }

        public void OnDisable() => _inputHandler.Clicked -= OnClicked;

        private void OnClicked(SlicedPart part)
        {
            Clicked?.Invoke();

            if (_minCountToDestroy == 0 && _slicedParts.Count > 0)
                MoveAllParts();
            else
                MoveSinglePart(part);
        }

        private void MoveAllParts()
        {
            _inputHandler.enabled = false;

            for (int i = 0; i < _slicedParts.Count; i++)
                MovePart(_slicedParts[i]);

            _slicedParts.Clear();
            Destroyed?.Invoke();
            AllPartsMoved?.Invoke();
            _inputHandler.Clicked -= OnClicked;
        }

        private void MoveSinglePart(SlicedPart part)
        {
            _minCountToDestroy--;
            _slicedParts.Remove(part);
            part.transform.SetParent(null);
            Vector3 partPosition = part.transform.position;
            MovePart(part);
            SinglePartMoved?.Invoke(part.transform);
        }

        private void MovePart(SlicedPart part)
            => _slicedPartAnimator.Move(_centr, part);

    }
}

using System;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation
{
    internal class InvertValidator : IPointValidator
    {
        private readonly IPointValidator _pointValidator;

        public InvertValidator(IPointValidator pointValidator) => 
            _pointValidator = pointValidator ?? throw new ArgumentException();

        public bool IsValid(Vector3 point) =>
            !_pointValidator.IsValid(point);
    }
}
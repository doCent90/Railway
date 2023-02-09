using System;
using System.Linq;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation
{
    internal class PointCompositeValidator : IPointValidator
    {
        private readonly IPointValidator[] _validators;

        public PointCompositeValidator(params IPointValidator[] validators) => 
            _validators = validators ?? throw new ArgumentException();

        public bool IsValid(Vector3 point) => 
            _validators.All(validator => validator.IsValid(point));
    }
}
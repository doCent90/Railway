using System;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation
{
    internal class PointInXBordersValidator : IPointValidator
    {
        private readonly float _minX;
        private readonly float _maxX;

        public PointInXBordersValidator(float minX, float maxX)
        {
            if (minX > maxX)
                throw new ArgumentException();
            
            _maxX = maxX;
            _minX = minX;
        }

        public bool IsValid(Vector3 point) => 
            point.x > _minX && point.x < _maxX;
    }
}
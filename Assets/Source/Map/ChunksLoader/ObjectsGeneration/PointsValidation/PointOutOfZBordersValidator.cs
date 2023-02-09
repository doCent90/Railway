using System;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation
{
    internal class PointOutOfZBordersValidator : IPointValidator
    {
        private readonly float _max;
        private readonly float _min;

        public PointOutOfZBordersValidator(float min, float max)
        {
            if (min > max)
                throw new ArgumentException();

            _min = min;
            _max = max;
        }

        public bool IsValid(Vector3 point) =>
            point.z > _max || point.z < _min;
    }
}
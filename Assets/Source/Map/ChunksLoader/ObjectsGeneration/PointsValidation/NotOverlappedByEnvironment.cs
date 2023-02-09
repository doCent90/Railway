using System;
using System.Linq;
using Source.Map.ChunksLoader.ObjectsGeneration.Environment;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation
{
    internal class NotOverlappedByEnvironment : IPointValidator
    {
        private readonly ObjectsContainer<ObjectWithBounds> _objectGenerator;

        public NotOverlappedByEnvironment(ObjectsContainer<ObjectWithBounds> objectGenerator) => 
            _objectGenerator = objectGenerator ?? throw new ArgumentException();

        public bool IsValid(Vector3 point) => 
            _objectGenerator.Objects.Any(bounds => bounds.Contains(point)) == false;
    }
}
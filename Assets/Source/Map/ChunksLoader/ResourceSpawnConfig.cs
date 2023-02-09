using System;
using System.Collections.Generic;
using Source.Map.ChunksLoader.ObjectsGeneration.Environment;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation;
using Source.Map.InteractableObjects;
using UnityEngine;

namespace Source.Map.ChunksLoader
{
    [Serializable]
    public class ResourceSpawnConfig
    {
        [field: SerializeField] public List<Resource> Templates { get; private set; }
        [field: SerializeField] public Vector2 PositionZDiscardResourceRange { get; private set; }

        public IPointValidator PointValidator =>
            new PointOutOfZBordersValidator(PositionZDiscardResourceRange.x, PositionZDiscardResourceRange.y);
    }
}
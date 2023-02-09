using System.Collections.Generic;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsProvider
{
    public interface IPointsProvider
    {
        IEnumerable<Vector3> Points { get; }
    }
}
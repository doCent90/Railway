using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation
{
    public interface IPointValidator
    {
        bool IsValid(Vector3 point);
    }
}
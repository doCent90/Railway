using UnityEngine;

namespace Source.Characters.Train
{
    public interface IMovablePath
    {
        bool CanMove(Vector3 position);
        bool Finished(Vector3 position);
        Vector3 StopPoint { get; }
    }
}
using UnityEngine;

namespace Source.Characters.Worker
{
    public interface IInputSource
    {
        Vector3 Destination { get; }
    }
}
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public interface IObjectFactory<T>
    {
        T Create(Vector3 position);
        void Destroy(T tObject);
        bool CanCreate(Vector3 vector3);
    }
}
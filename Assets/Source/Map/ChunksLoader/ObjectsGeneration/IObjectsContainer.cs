using System.Collections.Generic;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public interface IObjectsContainer<TObject> where TObject : Component
    {
        IEnumerable<TObject> Objects { get; }
        int Count { get; }
        void Add(TObject tObject);
        void Remove(TObject tObject);
        void Replace(TObject replaced, TObject newObject);
    }
}

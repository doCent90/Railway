using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public class ObjectsContainer<TObject> : IObjectsContainer<TObject> where TObject : Component
    {
        private readonly List<TObject> _objects = new();

        public IEnumerable<TObject> Objects => _objects;
        public int Count => _objects.Count;

        public void Add(TObject tObject)
        {
            if (tObject == null)
                throw new ArgumentException();

            _objects.Add(tObject);
        }

        public void Remove(TObject tObject) =>
            _objects.Remove(tObject);

        public void Replace(TObject replaced, TObject newObject)
        {
            int previousIndex = _objects.IndexOf(replaced);
            _objects.RemoveAt(previousIndex);
            _objects.Insert(previousIndex, newObject);
        }
    }
}

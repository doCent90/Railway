using System;
using System.Collections.Generic;
using System.Linq;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsProvider;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public class ObjectGenerator<TObject> where TObject : Component
    {
        private readonly ObjectsContainer<TObject> _objectsContainer;
        private readonly IObjectFactory<TObject> _objectFactory;
        private readonly IPointsProvider _pointsProvider;

        public ObjectGenerator(IPointsProvider pointsProvider, IObjectFactory<TObject> objectFactory,
            ObjectsContainer<TObject> container)
        {
            _pointsProvider = pointsProvider ?? throw new ArgumentException();
            _objectFactory = objectFactory ?? throw new ArgumentException();
            _objectsContainer = container ?? throw new ArgumentException();
        }

        public void Create(IPointValidator validator)
        {
            IEnumerable<Vector3> enumerable = _pointsProvider.Points.Where(validator.IsValid);

            foreach (Vector3 point in enumerable)
            {
                if(_objectFactory.CanCreate(point))
                    _objectsContainer.Add(_objectFactory.Create(point));
            }
        }

        public void Destroy(IPointValidator validator)
        {
            IEnumerable<TObject> enumerable =
                _objectsContainer.Objects.Where(tObject => validator.IsValid(tObject.transform.position)).ToArray();

            foreach (TObject tObject in enumerable)
            {
                _objectFactory.Destroy(tObject);
                _objectsContainer.Remove(tObject);
            }
        }
    }
}
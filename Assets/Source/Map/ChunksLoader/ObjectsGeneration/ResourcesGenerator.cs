using System;
using Source.Map.ChunksLoader.ObjectsGeneration.Environment;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation;
using Source.Map.InteractableObjects;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public class ResourcesGenerator : IMapObjectGenerator
    {
        private readonly ObjectGenerator<Resource> _objectGenerator;
        private readonly ObjectsContainer<ObjectWithBounds> _container;

        public ResourcesGenerator(ObjectGenerator<Resource> objectGenerator,
            ObjectsContainer<ObjectWithBounds> container)
        {
            _objectGenerator = objectGenerator ?? throw new ArgumentException();
            _container = container ?? throw new ArgumentException();
        }

        public void Create(float minX, float maxX)
        {
            _objectGenerator.Create(new PointCompositeValidator(
                new PointInXBordersValidator(minX, maxX), new PointOutOfZBordersValidator(-2f, 2f),
                new NotOverlappedByEnvironment(_container)));
        }

        public void Destroy(float minX, float maxX) =>
            _objectGenerator.Destroy(new PointInXBordersValidator(minX, maxX));
    }
}

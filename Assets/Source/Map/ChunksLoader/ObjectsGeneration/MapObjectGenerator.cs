using System;
using Source.Map.ChunksLoader.ObjectsGeneration.Environment;
using Source.Map.ChunksLoader.ObjectsGeneration.PointsValidation;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public class MapObjectGenerator : IMapObjectGenerator
    {
        private readonly ObjectGenerator<ObjectWithBounds> _objectGenerator;

        public MapObjectGenerator(ObjectGenerator<ObjectWithBounds> objectGenerator) => 
            _objectGenerator = objectGenerator?? throw new ArgumentException();

        public void Create(float minX, float maxX) => 
            _objectGenerator.Create(new PointInXBordersValidator(minX + 30f, maxX + 30f));

        public void Destroy(float minX, float maxX) => 
            _objectGenerator.Destroy(new PointInXBordersValidator(minX, maxX));
    }
}
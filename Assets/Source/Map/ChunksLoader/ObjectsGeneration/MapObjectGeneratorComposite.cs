using System;

namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    internal class MapObjectGeneratorComposite : IMapObjectGenerator
    {
        private readonly IMapObjectGenerator[] _mapObjectGenerator;

        public MapObjectGeneratorComposite(params IMapObjectGenerator[] mapObjectGenerator) =>
            _mapObjectGenerator = mapObjectGenerator ?? throw new ArgumentException();

        public void Create(float minX, float maxX)
        {
            foreach (IMapObjectGenerator objectGenerator in _mapObjectGenerator)
                objectGenerator.Create(minX, maxX);
        }

        public void Destroy(float minX, float maxX)
        {
            foreach (IMapObjectGenerator objectGenerator in _mapObjectGenerator)
                objectGenerator.Destroy(minX, maxX);
        }
    }
}
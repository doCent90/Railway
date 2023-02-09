using System;
using Source.Map.ChunksLoader.MapLoader;
using Source.Map.ChunksLoader.ObjectsGeneration;

namespace Source.Map.ChunksLoader
{
    class ChunkLoaderObjectGeneratorDecorator : IChunkLoader<MapChunk>
    {
        private readonly IChunkLoader<MapChunk> _chunkLoaderImplementation;
        private readonly IMapObjectGenerator _mapObjectGenerator;

        public ChunkLoaderObjectGeneratorDecorator(IMapObjectGenerator mapObjectGenerator, IChunkLoader<MapChunk> chunkLoaderImplementation)
        {
            _chunkLoaderImplementation = chunkLoaderImplementation;
            _mapObjectGenerator = mapObjectGenerator ?? throw new ArgumentException();
        }
        
        public MapChunk Load(int position)
        {
            MapChunk chunk = _chunkLoaderImplementation.Load(position);

            float halfWidth = chunk.Width / 2f;
            
            _mapObjectGenerator.Create(chunk.transform.position.x - halfWidth,
                chunk.transform.position.x + halfWidth);
            
            return chunk;
        }

        public void Unload(MapChunk chunk)
        {
            float halfWidth = chunk.Width / 2f;

            _mapObjectGenerator.Destroy(chunk.transform.position.x - halfWidth,
                chunk.transform.position.x + halfWidth);

            _chunkLoaderImplementation.Unload(chunk);
        }
    }
}
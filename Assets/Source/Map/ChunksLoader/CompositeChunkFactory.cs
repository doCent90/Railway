using System;
using System.Linq;

namespace Source.Map.ChunksLoader
{
    public class CompositeChunkFactory : IChunkFactory
    {
        private readonly IChunkFactory[] _chunkFactories;

        public CompositeChunkFactory(params IChunkFactory[] chunkFactory)
        {
            _chunkFactories = chunkFactory ?? throw new ArgumentNullException();
        }

        public MapChunk Create(MapChunk mapChunk) =>
            _chunkFactories.Aggregate(mapChunk, (current, chunkFactory) => chunkFactory.Create(current));

        public void Destroy(MapChunk chunk)
        {
            foreach (IChunkFactory chunkFactory in _chunkFactories)
                chunkFactory.Destroy(chunk);
        }
    }
}

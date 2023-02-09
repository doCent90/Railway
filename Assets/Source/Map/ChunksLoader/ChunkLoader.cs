using System;
using Source.Map.ChunksLoader.MapLoader;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Map.ChunksLoader
{
    public class ChunkLoader : IChunkLoader<MapChunk>
    {
        private readonly ScriptableMapConfig _locationConfig;
        private readonly IChunkFactory _chunkFactory;

        public ChunkLoader(ScriptableMapConfig locationConfig, IChunkFactory chunkFactory)
        {
            _locationConfig = locationConfig ? locationConfig : throw new ArgumentException();
            _chunkFactory = chunkFactory ?? throw new ArgumentException(); 
        }

        public MapChunk Load(int position)
        {
            LocationConfig config = LocationConfig.GetConfigByPosition(position, _locationConfig.LocationConfigs);

            MapChunk chunk = _chunkFactory.Create(config.ChunkConfig.GetRandomChunk());
            float halfWidth = config.ChunkWidth / 2f;
            chunk.transform.position = Vector3.right * (position + halfWidth);

            return chunk;
        }

        public void Unload(MapChunk chunk)
        {
            _chunkFactory.Destroy(chunk);
            Object.Destroy(chunk.gameObject);
        }
    }
}
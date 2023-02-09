using System;
using System.Collections.Generic;

namespace Source.Map.ChunksLoader.MapLoader
{
    public class MapLoader<TChunk>
    {
        private readonly List<SpawnedChunk<TChunk>> _oldChunks = new();
        private readonly IChunkLoader<TChunk> _chunkLoader;
        private readonly MapConfig _mapConfig;
        private readonly int _backBuffer;
        private readonly SpawnedChunk<TChunk>[] _chunks;

        public MapLoader(MapConfig mapConfig, IChunkLoader<TChunk> chunkLoader, int backBuffer, int forwardBuffer)
        {
            if (backBuffer < 1 || forwardBuffer < 1)
                throw new ArgumentException();

            _chunkLoader = chunkLoader ?? throw new ArgumentException();
            _mapConfig = mapConfig ?? throw new ArgumentException();
            _chunks = new SpawnedChunk<TChunk>[backBuffer + 1 + forwardBuffer];
            _backBuffer = backBuffer;
        }

        public bool CanLoadChunks()
        {
            SpawnedChunk<TChunk> chunk = _chunks[^1];

            if (chunk == null)
                return true;

            return chunk.Index < chunk.LocationConfig.Length - 1 ||
                   GetNextLocationConfig(chunk.LocationConfig) != null;
        }

        public void LoadChunks(float basePosition)
        {
            if (_chunks[_backBuffer] == null)
                LoadAllChunks(basePosition);

            if (_chunks[_backBuffer]?.EndBorder < basePosition)
                OffsetChunks();
        }

        public float GetClosestChunkEnd(float spawnX)
        {
            float x = 0f;

            for (int i = 0; i < _mapConfig.LocationConfigs.Length; i++)
            {
                ILocationConfig locationConfig = _mapConfig.LocationConfigs[i];

                for (int j = 0; j < locationConfig.Length; j++)
                {
                    if (i == _mapConfig.LocationConfigs.Length - 1 && j == locationConfig.Length - 1)
                        return x;

                    if (x >= spawnX)
                        return x;

                    x += locationConfig.ChunkWidth;
                }
            }

            return x;
        }

        public void UnloadChunks()
        {
            for (int i = 0; i < _oldChunks.Count; i++)
            {
                _chunkLoader.Unload(_oldChunks[0].Chunk);
                _oldChunks.RemoveAt(0);
            }
        }

        private void OffsetChunks()
        {
            if (CanLoadNext(_chunks[^1]) == false)
                return;

            _oldChunks.Add(_chunks[0]);

            for (int i = 0; i < _chunks.Length - 1; i++)
                _chunks[i] = _chunks[i + 1];

            _chunks[^1] = LoadNext(_chunks[^2]);
        }

        private void LoadAllChunks(float basePosition)
        {
            _chunks[0] = LoadNew(basePosition);

            for (var i = 1; i < _chunks.Length; i++)
            {
                if (CanLoadNext(_chunks[i - 1]) == false)
                    return;

                _chunks[i] = LoadNext(_chunks[i - 1]);
            }
        }

        private SpawnedChunk<TChunk> LoadNext(SpawnedChunk<TChunk> chunk)
        {
            if (chunk.Index == chunk.LocationConfig.Length - 1)
            {
                ILocationConfig nextLocationConfig = GetNextLocationConfig(chunk.LocationConfig);

                return new SpawnedChunk<TChunk>(_chunkLoader.Load(chunk.EndBorder), chunk.EndBorder,
                    nextLocationConfig, 0);
            }

            return new SpawnedChunk<TChunk>(_chunkLoader.Load(chunk.EndBorder), chunk.EndBorder,
                chunk.LocationConfig, chunk.Index + 1);
        }

        private bool CanLoadNext(SpawnedChunk<TChunk> chunk) =>
            chunk.Index != chunk.LocationConfig.Length - 1 || GetNextLocationConfig(chunk.LocationConfig) != null;

        private ILocationConfig GetNextLocationConfig(ILocationConfig chunkLocationConfig)
        {
            for (int i = 0; i < _mapConfig.LocationConfigs.Length - 1; i++)
            {
                if (_mapConfig.LocationConfigs[i] == chunkLocationConfig)
                    return _mapConfig.LocationConfigs[i + 1];
            }

            return null;
        }

        private SpawnedChunk<TChunk> LoadNew(float basePosition)
        {
            int position = 0;

            foreach (ILocationConfig locationConfig in _mapConfig.LocationConfigs)
            {
                for (int j = 0; j < locationConfig.Length; j++)
                {
                    int chunkWidth = locationConfig.ChunkWidth;

                    if (position + chunkWidth > basePosition)
                        return new SpawnedChunk<TChunk>(_chunkLoader.Load(position), position, locationConfig, j);

                    position += chunkWidth;
                }
            }

            return null;
        }
    }
}

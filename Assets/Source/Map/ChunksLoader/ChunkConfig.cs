using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Map.ChunksLoader
{
    [Serializable]
    public class ChunkConfig
    {
        [SerializeField] private List<MapChunk> _chunks;

        public MapChunk GetRandomChunk() =>
            _chunks[UnityEngine.Random.Range(0, _chunks.Count)];
    }
}
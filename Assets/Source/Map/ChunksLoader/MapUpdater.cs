using System;
using Source.Map.ChunksLoader.MapLoader;
using Source.Map.InteractableObjects;
using Source.Map.InteractableObjects.Cells;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Map.ChunksLoader
{
    public class MapUpdater
    {
        private readonly MapLoader<MapChunk> _mapLoader;
        private readonly Transform _loadTransform;
        private readonly float _startChunkLoadX;

        public MapUpdater(Transform loadTransform, MapLoader<MapChunk> mapLoader)
        {
            _loadTransform = loadTransform ? loadTransform : throw new ArgumentNullException();
            _mapLoader = mapLoader ?? throw new ArgumentException();
        }

        public void UpdateMap()
        {
            if (!_mapLoader.CanLoadChunks())
                return;

            _mapLoader.LoadChunks(_loadTransform.position.x);
            _mapLoader.UnloadChunks();
        }

        public void LoadFakeStartChunk(MapChunk startChunkTemplate, LocationType locationType, float startChunkLoadX)
        {
            int offset = (int)MathF.Round(startChunkLoadX);

            if (offset % 2 == 1)
                offset -= 1;
            else if (offset > startChunkLoadX)
                offset -= 2;

            MapChunk mapChunk = Object.Instantiate(startChunkTemplate, Vector3.right * offset, Quaternion.identity);

            foreach (Cell mapChunkCell in mapChunk.Cells)
                mapChunkCell.Construct(new AlwaysUnlocked(), locationType);

            foreach (Resource resource in mapChunk.Resources)
                resource.Construct(locationType);
        }
    }
}

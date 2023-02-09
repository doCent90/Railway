using System;
using Source.Map.ChunksLoader.MapLoader;
using UnityEngine;

namespace Source.Map.ChunksLoader
{
    [Serializable]
    public class LocationConfig : ILocationConfig
    {
        [field: SerializeField] public int Length { get; private set; }
        [field: SerializeField] public int ChunkWidth { get; private set; }
        [field: SerializeField] public ChunkConfig ChunkConfig { get; private set; }
        [field: SerializeField] public ResourceSpawnConfig ResourceSpawnConfig { get; private set; }

        public static LocationConfig GetConfigByPosition(int targetPosition, LocationConfig[] configs)
        {
            int position = 0;

            foreach (LocationConfig locationConfig in configs)
            {
                for (int j = 0; j < locationConfig.Length; j++)
                {
                    int chunkWidth = locationConfig.ChunkWidth;

                    if (position + chunkWidth > targetPosition)
                        return locationConfig;

                    position += chunkWidth;
                }
            }

            return null;
        }
    }
}
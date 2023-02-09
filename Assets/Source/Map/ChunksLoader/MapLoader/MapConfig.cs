using System;

namespace Source.Map.ChunksLoader.MapLoader
{
    [Serializable]
    public class MapConfig
    {
        public ILocationConfig[] LocationConfigs;

        public MapConfig(params ILocationConfig[] locationConfig)
        {
            LocationConfigs = locationConfig;
        }
    }
}
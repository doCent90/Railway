namespace Source.Map.ChunksLoader.MapLoader
{
    internal class SpawnedChunk<TChunk>
    {
        private readonly int _position;
        
        public readonly TChunk Chunk;
        public readonly ILocationConfig LocationConfig;
        public readonly int Index;

        public SpawnedChunk(TChunk chunk, int position, ILocationConfig locationConfig, int index)
        {
            _position = position;
            Chunk = chunk;
            LocationConfig = locationConfig;
            Index = index;
        }

        public int EndBorder => _position + LocationConfig.ChunkWidth;
    }
}
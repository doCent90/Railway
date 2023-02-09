namespace Source.Map.ChunksLoader
{
    public interface IChunkFactory
    {
        MapChunk Create(MapChunk mapChunk);
        void Destroy(MapChunk chunk);
    }
}
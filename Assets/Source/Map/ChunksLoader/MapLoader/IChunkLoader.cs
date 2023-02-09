namespace Source.Map.ChunksLoader.MapLoader
{
    public interface IChunkLoader<TChunk>
    {
        public TChunk Load(int position);
        public void Unload(TChunk chunk);
    }
}
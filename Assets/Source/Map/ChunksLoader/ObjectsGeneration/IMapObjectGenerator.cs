namespace Source.Map.ChunksLoader.ObjectsGeneration
{
    public interface IMapObjectGenerator
    {
        void Create(float minX, float maxX);
        void Destroy(float minX, float maxX);
    }
}
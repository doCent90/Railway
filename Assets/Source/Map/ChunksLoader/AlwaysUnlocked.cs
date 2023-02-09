namespace Source.Map.ChunksLoader
{
    internal class AlwaysUnlocked : ICellLock
    {
        public bool Locked => false;
    }
}
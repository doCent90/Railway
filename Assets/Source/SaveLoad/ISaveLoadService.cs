namespace Source.SaveLoad
{
    public interface ISaveLoadService
    {
        void Save();
        GameData Load();
    }
}
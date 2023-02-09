using System;

namespace Source.LevelLoaderService
{
    public interface ILevelLoader
    {
        void LoadLevel(int levelNumber);
        void LoadMenu();
        void LoadMenu(Action action);
        void LoadLevel(int levelNumber, Action action);
    }
}

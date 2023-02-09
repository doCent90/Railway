using System;

namespace Source.LevelLoaderService
{
    public interface ISceneLoader
    {
        void LoadScene(string name, Action onLoad);
        void LoadScene(int index, Action onLoad);
        void ReloadScene(Action onLoad);
    }
}

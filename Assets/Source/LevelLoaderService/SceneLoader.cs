using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.LevelLoaderService
{
    public class SceneLoader : ISceneLoader
    {
        public void LoadScene(string name, Action onLoad) =>
            Load(name, onLoad);

        public void LoadScene(int index, Action onLoad) =>
            Load(index, onLoad);

        public void ReloadScene(Action onLoad) =>
            Load(SceneManager.GetActiveScene().buildIndex, onLoad);

        private void Load(string name, Action onLoad)
        {
            AsyncOperation waitScene = SceneManager.LoadSceneAsync(name);
            SubscribeOnComplete(onLoad, waitScene);
        }

        private void Load(int index, Action onLoad)
        {
            AsyncOperation waitScene = SceneManager.LoadSceneAsync(index);
            SubscribeOnComplete(onLoad, waitScene);
        }

        private static void SubscribeOnComplete(Action onLoad, AsyncOperation waitScene) =>
            waitScene.completed += (a) => onLoad?.Invoke();
    }
}

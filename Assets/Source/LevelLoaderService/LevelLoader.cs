using System;
using Source.SaveLoad;

namespace Source.LevelLoaderService
{
    public class LevelLoader : ILevelLoader
    {
        private readonly IProgressService _progressService;
        private readonly ScenesConfiguration _sceneList;
        private readonly ISceneLoader _sceneLoader;

        public LevelLoader(ISceneLoader sceneLoader, IProgressService progressService, ScenesConfiguration sceneList)
        {
            _progressService = progressService ?? throw new ArgumentNullException();
            _sceneLoader = sceneLoader ?? throw new ArgumentNullException();
            _sceneList = sceneList ? sceneList : throw new ArgumentNullException();
        }

        public void LoadLevel(int levelNumber) =>
            LoadLevel(levelNumber, null);

        public void LoadMenu() =>
            LoadMenu(null);

        public void LoadMenu(Action action)
        {
            _progressService.GameData.LevelsData.MenuOpened = true;
            LoadScene(_sceneList.MenuScene.name, action);
        }

        public void LoadLevel(int levelNumber, Action action)
        {
            _progressService.GameData.LevelsData.Level = levelNumber;
            _progressService.GameData.LevelsData.MenuOpened = false;

            LoadScene(_progressService.GameData.LevelsData.Levels[levelNumber].Completed
                ? _sceneList.Levels[levelNumber].CompleteScene.name
                : _sceneList.Levels[levelNumber].BuildingScene.name, action);
        }

        private void LoadScene(string sceneName, Action onLoad) =>
            _sceneLoader.LoadScene(sceneName, onLoad);
    }
}

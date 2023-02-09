using System;

namespace Source.LevelLoaderService
{
    internal class NotifyLevelLoad : ILevelLoader
    {
        private readonly ILevelLoader _levelLoader;
        private readonly Boot _boot;

        public NotifyLevelLoad(Boot boot, ILevelLoader levelLoader)
        {
            _boot = boot;
            _levelLoader = levelLoader;
        }

        public void LoadLevel(int levelNumber) =>
            LoadLevel(levelNumber, null);

        public void LoadMenu() =>
            LoadMenu(null);

        public void LoadMenu(Action action) =>
            _levelLoader.LoadMenu(action + (() => _boot.OnLoad()));

        public void LoadLevel(int levelNumber, Action action) =>
            _levelLoader.LoadLevel(levelNumber, action + (() => _boot.OnLoad()));
    }
}

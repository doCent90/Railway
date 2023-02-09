using System;
using Source.LevelLoaderService;
using Source.SaveLoad;

namespace Source.Map
{
    public class BuildingLevelFinish : ILevelFinish
    {
        private readonly ILevelLoader _levelLoader;
        private readonly LevelData _levelData;

        public BuildingLevelFinish(ILevelLoader levelLoader, LevelData levelData)
        {
            _levelData = levelData ?? throw new ArgumentNullException();
            _levelLoader = levelLoader ?? throw new ArgumentNullException();
        }

        public void Finish()
        {
            _levelLoader.LoadMenu();
            _levelData.Completed = true;
            _levelData.CompleteLevelData.LastIncomeClaim = DateTime.Now;
        }
    }
}

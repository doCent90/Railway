using System;
using Source.Characters;
using Source.Map.ChunksLoader;
using Source.Money;
using Source.Tutorials;
using Source.UI;

namespace Source.Map.InteractableObjects.Cells
{
    public class BuildableStationInitializer : IChunkFactory
    {
        private readonly DynamicCamera _camera;
        private readonly IHidableView _gameUI;
        private readonly ITutorialRunner _tutorial;
        private readonly ILevelFinish _buildingLevelFinish;
        private readonly IEnvironmentPayer _payer;

        public BuildableStationInitializer(ITutorialRunner tutorial, IHidableView gameUI, DynamicCamera camera,
            ILevelFinish buildingLevelFinish, IEnvironmentPayer payer)
        {
            _payer = payer;
            _tutorial = tutorial ?? throw new ArgumentNullException();
            _buildingLevelFinish = buildingLevelFinish;
            _camera = camera;
            _gameUI = gameUI;
        }

        public MapChunk Create(MapChunk mapChunk)
        {
            BuildableStation station = mapChunk.GetComponentInChildren<BuildableStation>(true);

            if (station != null)
                Init(station);

            return mapChunk;
        }

        private void Init(BuildableStation station) =>
            station.Construct(_tutorial, _gameUI, _camera, _buildingLevelFinish, _payer);

        public void Destroy(MapChunk chunk)
        {
        }
    }
}

using System;
using Source.Map.InteractableObjects.Cells;
using Source.Tutorials;

namespace Source.Map.ChunksLoader
{
    public class ChunkTutorialInitializer : IChunkFactory
    {
        private readonly ITutorialRunner _tutorialRunner;

        public ChunkTutorialInitializer(ITutorialRunner tutorialRunner)
        {
            _tutorialRunner = tutorialRunner ?? throw new ArgumentNullException();
        }

        public MapChunk Create(MapChunk mapChunk)
        {
            BridgeTutorial bridgeTutorial = mapChunk.GetComponentInChildren<BridgeTutorial>(true);

            if (bridgeTutorial != null)
                _tutorialRunner.Run(bridgeTutorial);

            StoneCell stoneCell = mapChunk.GetComponentInChildren<StoneCell>();

            if (stoneCell != null)
                _tutorialRunner.Run(stoneCell);

            return mapChunk;
        }

        public void Destroy(MapChunk chunk)
        {
        }
    }
}

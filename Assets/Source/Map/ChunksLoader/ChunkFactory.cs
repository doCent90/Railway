using System;
using System.Collections.Generic;
using Source.Map.InteractableObjects;
using Source.Map.InteractableObjects.Cells;
using Source.Tutorials;
using Object = UnityEngine.Object;

namespace Source.Map.ChunksLoader
{
    public class ChunkFactory : IChunkFactory
    {
        private const int InitializedCellsMaxAmount = 5;

        private readonly IInteractablesContainer _interactablesContainer;
        private readonly IInteractablesContainer _railwayContainer;
        private readonly CellWay _cellWay;
        private readonly BuildableStationInitializer _initializer;
        private readonly LocationType _locationType;
        private readonly Queue<Cell> _cellInitializationQueue = new();

        private ICellLock _lastCellLock = new AlwaysUnlocked();
        private ITutorialRunner _tutorialRunner;

        public ChunkFactory(IInteractablesContainer resourceContainer, ITutorialRunner tutorialRunner,
            IInteractablesContainer railwayContainer,
            CellWay cellWay, BuildableStationInitializer initializer, LocationType locationType)
        {
            _locationType = locationType;
            _tutorialRunner = tutorialRunner ?? throw new ArgumentNullException();
            _initializer = initializer ?? throw new ArgumentException();
            _cellWay = cellWay ?? throw new ArgumentException();
            _railwayContainer = railwayContainer ?? throw new ArgumentException();
            _interactablesContainer = resourceContainer ?? throw new ArgumentException();
        }

        public void Update()
        {
            while (CanInitializeCell())
                InitializeCell();
        }

        private void InitializeCell()
        {
            Cell cell = _cellInitializationQueue.Dequeue();
            _railwayContainer.Add(cell);
            _cellWay.Add(cell);
        }

        private bool CanInitializeCell()
        {
            return _cellInitializationQueue.Count > 0 && _cellInitializationQueue.Peek() != null &&
                   _cellWay.GetNotFinishedAmount() < InitializedCellsMaxAmount;
        }

        public MapChunk Create(MapChunk mapChunk)
        {
            MapChunk chunk = Object.Instantiate(mapChunk);

            foreach (Resource resource in chunk.Resources)
            {
                _interactablesContainer.Add(resource);
                resource.Construct(_locationType);
            }

            foreach (Cell cell in chunk.Cells)
            {
                cell.Construct(_lastCellLock, _locationType);
                _cellInitializationQueue.Enqueue(cell);
                _lastCellLock = cell;
            }

            return chunk;
        }

        public void Destroy(MapChunk chunk)
        {
            foreach (Resource resource in chunk.Resources)
                _interactablesContainer.Remove(resource);

            foreach (Cell cell in chunk.Cells)
                _railwayContainer.Remove(cell);
        }
    }
}

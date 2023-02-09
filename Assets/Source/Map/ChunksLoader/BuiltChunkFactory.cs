using System;
using Source.Map.InteractableObjects;
using Source.Map.InteractableObjects.Cells;
using Source.Stack;
using Object = UnityEngine.Object;

namespace Source.Map.ChunksLoader
{
    internal class BuiltChunkFactory : IChunkFactory
    {
        private readonly IInteractablesContainer _interactablesContainer;
        private readonly LocationType _location;
        private IStackableTypes _spawnableStackables;

        public BuiltChunkFactory(IInteractablesContainer resourceContainer, LocationType location,
            IStackableTypes spawnableStackables)
        {
            _location = location;
            _spawnableStackables = spawnableStackables ?? throw new ArgumentNullException();
            _interactablesContainer = resourceContainer ?? throw new ArgumentNullException();
        }

        public MapChunk Create(MapChunk mapChunk)
        {
            MapChunk chunk = Object.Instantiate(mapChunk);

            foreach (Resource resource in chunk.Resources)
            {
                if (_spawnableStackables.Contains(resource.StackableType))
                    _interactablesContainer.Add(resource);

                resource.Construct(_location);
            }

            foreach (Cell cell in chunk.Cells)
                cell.Construct(cell, _location);

            return chunk;
        }

        public void Destroy(MapChunk chunk)
        {
            foreach (Resource resource in chunk.Resources)
                _interactablesContainer.Remove(resource);
        }
    }
}

using System;
using System.Linq;
using Source.Map.ChunksLoader;
using Source.Map.ChunksLoader.ObjectsGeneration;
using Source.Stack;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Map.InteractableObjects
{
    class ResourceFactory : IObjectFactory<Resource>
    {
        private readonly ScriptableMapConfig _locationConfig;
        private readonly IInteractablesContainer _container;
        private readonly LocationType _location;
        private readonly StackableTypes _interactableTypes;

        public ResourceFactory(ScriptableMapConfig scriptableMapConfig, IInteractablesContainer container,
            LocationType location, StackableTypes interactableTypes)
        {
            _interactableTypes = interactableTypes;
            _location = location;
            _container = container ?? throw new ArgumentException();
            _locationConfig = scriptableMapConfig ? scriptableMapConfig : throw new ArgumentException();
        }

        public bool CanCreate(Vector3 position)
        {
            ResourceSpawnConfig spawnConfig = GetConfigByPosition(position).ResourceSpawnConfig;

            return spawnConfig.Templates.Any(template => _interactableTypes.Contains(template.StackableType)) &&
                   spawnConfig.PointValidator.IsValid(position);
        }

        public Resource Create(Vector3 position)
        {
            LocationConfig config = GetConfigByPosition(position);

            Resource template =
                config.ResourceSpawnConfig.Templates[Random.Range(0, config.ResourceSpawnConfig.Templates.Count)];

            Resource resource = Object.Instantiate(template, position, Quaternion.identity);
            resource.Construct(_location);

            if (_interactableTypes.Contains(resource.StackableType))
                _container.Add(resource);

            return resource;
        }

        private LocationConfig GetConfigByPosition(Vector3 position) =>
            LocationConfig.GetConfigByPosition((int)position.x, _locationConfig.LocationConfigs);

        public void Destroy(Resource resource)
        {
            _container.Remove(resource);
            Object.Destroy(resource.gameObject);
        }
    }
}

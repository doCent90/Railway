using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Source.Map.ChunksLoader.ObjectsGeneration.Environment
{
    internal class EnvironmentObjectFactory : IObjectFactory<ObjectWithBounds>
    {
        private readonly IEnumerable<DistanceUnlock> _distanceUnlockables;

        public EnvironmentObjectFactory(IEnumerable<DistanceUnlock> distanceUnlocks) =>
            _distanceUnlockables = distanceUnlocks;

        public bool CanCreate(Vector3 vector3) => 
            _distanceUnlockables.Any();

        public ObjectWithBounds Create(Vector3 vector3)
        {
            DistanceUnlock[] unlocked = _distanceUnlockables.Where(unlock => unlock.Distance < vector3.x).ToArray();
            ObjectWithBounds template = unlocked[Random.Range(0, unlocked.Length)].Object;

            return Object.Instantiate(template, vector3, Quaternion.identity);
        }

        public void Destroy(ObjectWithBounds tObject)
        {
        }

        [Serializable]
        internal class DistanceUnlock
        {
            [field: SerializeField] public ObjectWithBounds Object { get; private set; }
            [field: SerializeField] public int Distance { get; private set; }
        }
    }
}
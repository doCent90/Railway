using System.Collections.Generic;
using UnityEngine;

namespace Source.Map.ChunksLoader.ObjectsGeneration.Environment
{
    [CreateAssetMenu(menuName = "Create DistanceUnlockablesList", fileName = "DistanceUnlockablesList", order = 0)]
    internal class DistanceUnlockablesList : ScriptableObject
    {
        [SerializeField] private List<EnvironmentObjectFactory.DistanceUnlock> _unlockables;
        
        public IEnumerable<EnvironmentObjectFactory.DistanceUnlock> DistanceUnlockables => _unlockables;
    }
}
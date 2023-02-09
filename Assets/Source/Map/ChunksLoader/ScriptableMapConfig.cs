using UnityEngine;

namespace Source.Map.ChunksLoader
{
    [CreateAssetMenu(menuName = "Create ScriptableMapConfig", fileName = "ScriptableMapConfig", order = 0)]
    public class ScriptableMapConfig : ScriptableObject
    {
        [field: SerializeField] public LocationConfig[] LocationConfigs { get; private set; }
    }
}
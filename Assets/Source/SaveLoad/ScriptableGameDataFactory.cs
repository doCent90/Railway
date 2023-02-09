using UnityEngine;

namespace Source.SaveLoad
{
    [CreateAssetMenu(menuName = "Create ScriptableGameDataFactory", fileName = "ScriptableGameDataFactory", order = 0)]
    internal class ScriptableGameDataFactory : ScriptableObject, IGameDataFactory
    {
        [SerializeField] private GameData _gameData;

#if UNITY_EDITOR
        [SerializeField] private bool _debug;
        [SerializeField] private GameData _debugData;
#endif

        public GameData Create()
        {
#if UNITY_EDITOR
            if (_debug)
                return _debugData;
#endif
            return _gameData;
        }
    }
}
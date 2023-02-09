using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Source.LevelLoaderService
{
    [CreateAssetMenu(menuName = "Create LevelScenesConfiguration", fileName = "LevelScenesConfiguration", order = 0)]
    public class ScenesConfiguration : ScriptableObject
    {
        [field: SerializeField] public Scene MenuScene { get; private set; }
        [field: SerializeField] public LevelScenes[] Levels { get; private set; }

#if UNITY_EDITOR
        [ContextMenu(nameof(GetScenesFromAssets))]
        public void GetScenesFromAssets()
        {
            foreach (LevelScenes levelScenes in Levels)
            {
                levelScenes.BuildingScene = new Scene() {name = levelScenes.BuildingSceneAsset.name};
                levelScenes.CompleteScene = new Scene() {name = levelScenes.CompleteSceneAsset.name};
            }
        }
#endif
    }

    [Serializable]
    public class LevelScenes
    {
        [field: SerializeField] public Scene BuildingScene { get; set; }
        [field: SerializeField] public Scene CompleteScene { get; set; }

#if UNITY_EDITOR
        [field: SerializeField] public SceneAsset BuildingSceneAsset { get; private set; }
        [field: SerializeField] public SceneAsset CompleteSceneAsset { get; private set; }
#endif
    }

    [Serializable]
    public class Scene
    {
        public string name;
    }
}

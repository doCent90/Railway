using UnityEngine;

namespace Source.Progress.Tasks
{
    [CreateAssetMenu(menuName = "Create DistanceTaskView", fileName = "DistanceTaskView", order = 0)]
    public class DistanceTaskView : ScriptableObject, ITaskView
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
    }
}
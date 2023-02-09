using UnityEngine;

namespace Source.Characters.Upgrades
{
    [CreateAssetMenu(menuName = "Create ScriptableLevelFloatProgression", fileName = "ScriptableLevelFloatProgression", order = 0)]
    internal class ScriptableLevelFloatProgression : ScriptableObject
    {
        [SerializeField] private LevelFloatProgression _incomeFloatProgression;

        public LevelFloatProgression IncomeProgression => _incomeFloatProgression;
    }
}
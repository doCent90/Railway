using Source.Characters.Upgrades;
using UnityEngine;

namespace Source.Characters.Worker
{
    [CreateAssetMenu(menuName = "Create WorkerStats", fileName = "WorkerStats", order = 0)]
    public class WorkerStats : ScriptableObject
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _interactionSpeed;
        [SerializeField] private LevelFloatProgression _speedUpgradeProgression;
        [SerializeField] private float _chopSpeed;

        private UpgradeValue<float> _speedUpgrade;

        public float Speed => _speed * _speedUpgrade.Value;
        public float InteractionSpeed => _interactionSpeed * _speedUpgrade.Value;
        public float ChopSpeed => _chopSpeed;

        public void Construct(UpgradeLevel upgradeLevel) =>
            _speedUpgrade = new UpgradeValue<float>(upgradeLevel, _speedUpgradeProgression);
    }
}
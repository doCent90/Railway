using System;
using System.Collections.Generic;
using System.Linq;
using Source.Characters.Upgrades;
using Source.GlobalMap;
using Source.SaveLoad;

namespace Source.UI.UpdatableView
{
    public class OfflineIncomeCollectedCondition : IUpdateCondition
    {
        private readonly List<OfflineIncomeHelper> _offlineIncomeHelpers = new();
        private readonly LevelsData _levelsData;
        private readonly LevelData _levelData;

        public OfflineIncomeCollectedCondition(LevelsData levelsData, LevelData levelData)
        {
            _levelData = levelData ?? throw new ArgumentNullException();
            _levelsData = levelsData ?? throw new ArgumentNullException();
        }

        public bool Updated => _offlineIncomeHelpers.Any(income => income.FullProgress);

        public void InitializeOfflineIncome()
        {
            foreach (LevelData level in _levelsData.Levels)
            {
                if (level.Completed == false)
                    return;

                if (level == _levelData)
                    continue;

                ResourceCapacityValueFactory capacityFactory =
                    new ResourceCapacityValueFactory(level.CompleteLevelData.UpgradeLevels[1]);

                ResourceCostFactory costFactory =
                    new ResourceCostFactory(level.CompleteLevelData.UpgradeLevels[0]);

                IncomeFactory incomeFactory =
                    new IncomeFactory(level.CompleteLevelData.UpgradeLevels[2]);

                UpgradeValue<int> capacity = capacityFactory.Create();
                UpgradeValue<float> cost = costFactory.Create();
                UpgradeValue<float> incomeUpgradeValue = incomeFactory.Create();

                _offlineIncomeHelpers.Add(new OfflineIncomeHelper(level.CompleteLevelData, cost, capacity,
                    incomeUpgradeValue));
            }
        }
    }
}

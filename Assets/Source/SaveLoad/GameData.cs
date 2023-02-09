using System;
using Source.Characters.Upgrades;
using Source.Money;
using Source.Progress.Tasks;

namespace Source.SaveLoad
{
    [Serializable]
    public class GameData
    {
        public LevelsData LevelsData;
        public MoneyBalance MoneyBalance;
        public AnalyticsData AnalyticsData;
    }

    [Serializable]
    public class AnalyticsData
    {
        public int SessionId;
        public string RegDay;
        public string AllPlayTime;
    }

    [Serializable]
    public class LevelData
    {
        public UpgradeLevel TrainUpgrade;
        public UpgradeLevel[] UpgradeLevels;
        public UpgradeLevel MergeUpgradeLevel;
        public DistanceTask[] DistanceTasks;
        public DistanceTraveled TraveledDistance;
        public bool Completed;
        public CompleteLevelData CompleteLevelData;
    }

    [Serializable]
    public class CompleteLevelData
    {
        public UpgradeLevel[] UpgradeLevels;
        public UpgradeLevel MergeUpgradeLevel;
        public DistanceTraveled TraveledDistance;
        public DateTime LastIncomeClaim;
        public int Claims;
        public DistanceTask[] DistanceTasks;
    }

    [Serializable]
    public class DistanceTraveled
    {
        public float Distance;
    }

    [Serializable]
    public class LevelsData
    {
        public int Level;
        public LevelData[] Levels;
        public bool MenuOpened;

        public void AddLevel() =>
            Level++;
    }
}

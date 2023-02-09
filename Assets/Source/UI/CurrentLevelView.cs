using Source.Extensions;
using Source.GlobalMap;
using Source.SaveLoad;
using UnityEngine;

namespace Source.UI
{
    public class CurrentLevelView
    {
        private const string FirstUnlockRegion = nameof(FirstUnlockRegion);

        private readonly RegionUnlockAnimator _regionUnlockAnimator;
        private readonly CanvasGroup _buildInProgress;
        private readonly CanvasGroup _allCanvasUI;
        private readonly CanvasGroup _stationIcon;
        private readonly CanvasGroup _build;
        private readonly CanvasGroup _train;
        private readonly LevelsData _levelsData;
        private readonly MapRoad _mapRoad;
        private readonly Region _region;
        private readonly int _level;

        public CurrentLevelView(Region region, LevelsData gameData, int level,
            CanvasGroup allCanvasUI, CanvasGroup build, CanvasGroup train, CanvasGroup buildInProgress, MapRoad mapRoad, Camera camera, CanvasGroup stationIcon)
        {
            _buildInProgress = buildInProgress;
            _allCanvasUI = allCanvasUI;
            _levelsData = gameData;
            _mapRoad = mapRoad;
            _region = region;
            _level = level;
            _build = build;
            _train = train;
            _stationIcon = stationIcon;
            _regionUnlockAnimator = new RegionUnlockAnimator(camera, region.LockedRegion, region.Lock, region.UnLock, region.LocksContainer, _stationIcon, _mapRoad, _build);
        }

        public void EnableView()
        {
            string key = FirstUnlockRegion + _level;
            LevelData level = _levelsData.Levels[_level];

            if (_level == 0)
                Enable(level);
            else if (_levelsData.Levels[_level].Completed)
                Enable(level);
            else if (PlayerPrefs.HasKey(key) == false && _levelsData.Levels[_level].Completed == false && _levelsData.Levels[_level - 1].Completed)
                UnlockIfNext(key);
            else if (PlayerPrefs.HasKey(key) && _levelsData.Levels[_level].Completed == false && _levelsData.Levels[_level - 1].Completed)
                Enable(level);
            else
                Disable();
        }

        private void UnlockIfNext(string key)
        {
            _regionUnlockAnimator.Animate(key);
            _train.DisableGroup(0);
            _buildInProgress.DisableGroup(0);
        }

        private void Enable(LevelData level)
        {
            _region.Open();
            _allCanvasUI.EnableGroup();
            _train.DisableGroup();
            _build.DisableGroup();
            _buildInProgress.DisableGroup();

            if (level.Completed)
            {
                _train.EnableGroup();
                _mapRoad.Complete();
                return;
            }

            if (level.TraveledDistance.Distance == 0)
            {
                _build.EnableGroup();
            }
            else
            {
                _mapRoad.Complete();
                _buildInProgress.EnableGroup();
            }
        }

        private void Disable()
        {
            _allCanvasUI.DisableGroup(duration: 0);
            _region.Close();
            _mapRoad.Disable();
        }
    }
}

using Source.Extensions;
using Source.GlobalMap;
using Source.SaveLoad;
using Source.Common;
using UnityEngine.UI;
using UnityEngine;
using System;
using System.Collections;
using RunnerMovementSystem;
using RunnerMovementSystem.Model;
using TMPro;

namespace Source.UI
{
    [Serializable]
    public class GlobalProgressView : IGlobalProgressView
    {
        private const float WaitTime = 2f;

        [Header("Canvas groups")] [SerializeField]
        private CanvasGroup _uICanvasGroup;

        [SerializeField] private CanvasGroup _trainGroup;
        [SerializeField] private CanvasGroup _claimButtonGroup;
        [SerializeField] private CanvasGroup _buidCanvas;
        [SerializeField] private CanvasGroup _buildInProgressCanvas;
        [SerializeField] private CanvasGroup _stationIconCanvas;

        [Header("Point positions")] [SerializeField]
        private Transform _trainMovement;

        [SerializeField] private Transform _trainIconPunch;

        [Header("Buttons")] [SerializeField]
        private TMP_Text _claimText;

        [SerializeField] private MapRoad _mapRoad;
        [SerializeField] private MovementOptions _movementOptions;

        private GlobalTrainAnimation _globalTrainAnimation;
        private OfflineIncomeHelper _offlineIncomeHelper;
        private CurrentLevelView _currentLevelView;
        private LevelData _levelData;
        private AccumulatedIncomeProvider _accumulatedIncomeProvider;
        private ICurrentTrainView _currentTrainView;

        private int _maxReward;
        private MovementBehaviour _movementBehaviour;
        private float _spentTime;

        public void Construct(LevelData levelData, OfflineIncomeHelper offlineIncomeHelper, LevelsData levelsData,
            Region region, int level, AccumulatedIncomeProvider accumulatedIncomeProvider,
            ICurrentTrainView currentTrainView, Camera camera)
        {
            _accumulatedIncomeProvider = accumulatedIncomeProvider;
            _levelData = levelData ?? throw new ArgumentNullException();
            _offlineIncomeHelper = offlineIncomeHelper ?? throw new ArgumentNullException();
            _currentTrainView = currentTrainView ?? throw new ArgumentNullException();

            _movementBehaviour = new MovementBehaviour(_trainMovement, _movementOptions);
            _movementBehaviour.Init(_mapRoad.Path);
            _currentLevelView = new CurrentLevelView(region, levelsData, level, _uICanvasGroup, _buidCanvas,
                _trainGroup, _buildInProgressCanvas, _mapRoad, camera, _stationIconCanvas);
            _globalTrainAnimation = new GlobalTrainAnimation(_trainIconPunch);

            _currentLevelView.EnableView();
        }

        public void OnCurrentTrainShow(IButtonUI buttonUi) => _currentTrainView.Show(_maxReward, buttonUi);

        public void Update()
        {
            if (_levelData.Completed == false)
            {
                SetProgress(0);
                DisableClaimButton();
                return;
            }

            TimeSpan time = _offlineIncomeHelper.GetRemainingTime();

            if (time.Ticks < 0)
            {
                SetRewardText();
                SetProgress(1);
                return;
            }

            UpdateProgress();
        }

        private void UpdateProgress()
        {
            float progress = _offlineIncomeHelper.GetNormalizedProgress();
            SetProgress(progress);

            if (_spentTime < 0)
            {
                SetRewardText();
                _spentTime = WaitTime;
            }
            else
            {
                _spentTime -= Time.deltaTime;
            }
        }

        private void SetProgress(float progress)
        {
            _globalTrainAnimation.Play(progress);

            if (progress < 1)
            {
                _movementBehaviour.MoveForward();
                _movementBehaviour.UpdateTransform();
            }
        }

        private void SetRewardText()
        {
            float progress = _offlineIncomeHelper.GetNormalizedProgress();
            int maxCapacity = (int)_offlineIncomeHelper.GetMaxIncomeClaimCapacity();
            _maxReward = maxCapacity;

            _claimText.text = $"{_accumulatedIncomeProvider.GetForProgress(progress)}";
        }

        private void DisableClaimButton() => _claimButtonGroup.DisableGroup();
    }
}

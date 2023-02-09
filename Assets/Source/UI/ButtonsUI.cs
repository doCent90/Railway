using System;
using System.Collections.Generic;
using Source.Analytics;
using Source.Common;
using Source.Characters.Upgrades;
using Source.GlobalMap;
using Source.LevelLoaderService;
using UnityEngine.UI;
using Source.Money;
using UnityEngine;
using System.Collections;

namespace Source.UI
{
    [Serializable]
    public class ButtonsUI : IButtonUI
    {
        [SerializeField] private Button _buildLevelEnterButton;
        [SerializeField] private Button _buildLevelInProgressEnterButton;
        [SerializeField] private Button _trainEnterButton;
        [SerializeField] private Button _claimButton;

        private OfflineIncomeHelper _offlineIncomeHelper;
        private LoadingScreen _loadingScreen;
        private UpgradeValue<int> _upgradeValue;
        private IAnalyticsService _analyticsService;
        private IEnvironmentPayer _moneyPayerGlobal;
        private ICoroutineRunner _coroutine;
        private ILevelLoader _levelLoader;
        private IGlobalProgressView _globalProgressView;
        private IGlobalMapUI _globalMapUI;

        private int _levelIndex;
        private bool _hasClicked;

        public void Construct(UpgradeValue<int> upgradeValue, IEnvironmentPayer moneyPayerGlobal,
            ILevelLoader levelLoader, int levelIndex, OfflineIncomeHelper offlineIncomeHelper,
            LoadingScreen loadingScreen, IAnalyticsService analyticsService, ICoroutineRunner coroutine,
            IGlobalProgressView globalProgressView, IGlobalMapUI globalMapUI)
        {
            _analyticsService = analyticsService;
            _offlineIncomeHelper = offlineIncomeHelper ?? throw new ArgumentNullException();
            _upgradeValue = upgradeValue;
            _moneyPayerGlobal = moneyPayerGlobal;
            _levelLoader = levelLoader;
            _levelIndex = levelIndex;
            _loadingScreen = loadingScreen;
            _coroutine = coroutine;
            _globalProgressView = globalProgressView;
            _globalMapUI = globalMapUI;
        }

        public void SubscribeButtons()
        {
            _buildLevelEnterButton.onClick.AddListener(LoadLevel);
            _buildLevelInProgressEnterButton.onClick.AddListener(LoadLevel);
            _trainEnterButton.onClick.AddListener(OpenTrainFrame);
            _claimButton.onClick.AddListener(AddClaimedReward);
        }

        public void UnSubscribeButtons()
        {
            _buildLevelEnterButton.onClick.RemoveListener(LoadLevel);
            _buildLevelInProgressEnterButton.onClick.RemoveListener(LoadLevel);
            _trainEnterButton.onClick.RemoveListener(OpenTrainFrame);
            _claimButton.onClick.RemoveListener(AddClaimedReward);
        }

        public void LoadLevel()
        {
            _coroutine.StartCoroutine(Delay());

            IEnumerator Delay()
            {
                _globalMapUI.OnLoadStarted();
                _loadingScreen.Enable();
                yield return new WaitForSecondsRealtime(1f);
                _levelLoader.LoadLevel(_levelIndex);
            }
        }

        private void OpenTrainFrame() => _globalProgressView.OnCurrentTrainShow(this);

        private void AddClaimedReward()
        {
            if (_hasClicked == false)
                _coroutine.StartCoroutine(RewardDelay());
            else
                return;

            Dictionary<string, object> properties = new()
            {
                {"level", _levelIndex}, {"amount", (int)_upgradeValue.Value}
            };

            _hasClicked = true;
            _analyticsService.LogEvent("claim", properties);
            _moneyPayerGlobal.Pay((int)_offlineIncomeHelper.Collected, _claimButton.transform.position);
            _offlineIncomeHelper.Claim();
            _claimButton.interactable = false;
        }

        private IEnumerator RewardDelay()
        {
            yield return new WaitForSecondsRealtime(2f);
            _hasClicked = false;
            _claimButton.interactable = true;
        }
    }
}

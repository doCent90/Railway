using DG.Tweening;
using Source.Characters.Train;
using Source.Characters.Upgrades;
using Source.Extensions;
using Source.LevelLoaderService;
using Source.Map.InteractableObjects;
using Source.Money;
using Source.UI.UpdatableView;
using UnityEngine;

namespace Source.UI
{
    public class GameUI : MonoBehaviour, IHidableView
    {
        [SerializeField] private MoneyView _moneyView;
        [SerializeField] private MoneyPayView _moneyPayView;
        [SerializeField] private MergeButtonActivator _mergeButtonActivator;
        [SerializeField] private UpgradePurchase[] _upgradePurchases;
        [SerializeField] private UpgradePurchase _mergeUpgrade;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private CanvasGroup _levelComplete;
        [SerializeField] private TaskView _taskView;
        [SerializeField] private LaunchMapButton _launchMapButton;
        [SerializeField] private UpdatedView _updatedView;
        [SerializeField] private SellButton _sellButton;
        [SerializeField] private TapTutorial _tutorial;
        [SerializeField] private GlobalMoneyPayView _globalMoneyPayView;
        [SerializeField] private Transform _moneyIcon;

        private MoneyBalance _moneyBalance;
        private UpgradeLevel[] _upgradeLevels;
        public TaskView TaskView => _taskView;
        public IEnvironmentPayer PayView => _moneyPayView;
        public SellButton SellButton => _sellButton;
        public TrainCompleteMovement TrainMovement { get; private set; }
        public ITutorial TapTutorial => _tutorial;
        public IEnvironmentPayer GlobalMoneyPayView => _globalMoneyPayView;

        public void Construct(MoneyBalance moneyBalance, UpgradeLevel[] upgradeLevels, int workerVariantsCount,
            UpgradeLevel mergeUpgradeLevel, ILevelLoader levelLoader, LocationType location,
            ICanBuyCondition[] canBuyConditions, IUpdateCondition updateCondition, TrainCompleteMovement trainMovement = null)
        {
            _moneyBalance = moneyBalance;
            _upgradeLevels = upgradeLevels;
            TrainMovement = trainMovement;
            _moneyView.Construct(_moneyBalance);
            _moneyPayView.Construct(_moneyBalance);
            _taskView.SetLocation(location);
            _globalMoneyPayView.Construct(_moneyBalance, _moneyIcon);

            for (var i = 0; i < _upgradePurchases.Length; i++)
                _upgradePurchases[i].Construct(_moneyBalance, _upgradeLevels[i], canBuyConditions[i]);

            _mergeButtonActivator.Construct(_upgradeLevels[1], mergeUpgradeLevel, workerVariantsCount);
            _mergeUpgrade.Construct(moneyBalance, mergeUpgradeLevel, new CanAlwaysBuy());
            _launchMapButton.Construct(levelLoader);
            _updatedView.Construct(updateCondition);
        }

        public void Hide() => _canvasGroup.DisableGroup();

        public void Show() => _canvasGroup.EnableGroup();

        public void Show(bool isDone) => _levelComplete.EnableGroup();

        private void OnDestroy() => _canvasGroup.DOComplete();
    }
}

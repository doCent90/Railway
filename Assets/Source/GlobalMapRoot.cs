using Source.Characters.Upgrades;
using Source.GlobalMap;
using Source.UI;
using UnityEngine;

namespace Source
{
    public class GlobalMapRoot : BaseCompositionRoot
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private GlobalMapUI _globalMapUI;
        [SerializeField] private MoneyView _moneyView;
        [SerializeField] private Transform _moneyPosition;
        [SerializeField] private RegionsHolder _regionsHolder;
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private GlobalMoneyView _globalMoneyView;
        [SerializeField] private GlobalMapScroller _globalMapScroller;
        [SerializeField] private GlobalMoneyPayView _moneyGlobalPayView;
        [SerializeField] private TrainSpeedProgression _trainSpeedProgression;
        [SerializeField] private GlobalUICurrentTrainView _trainUICurrentTrainView;

        protected override void InitializeLevel()
        {
            _globalMapUI.Construct(_regionsHolder, _moneyGlobalPayView, _moneyView, MoneyBalance,
                ProgressService, LevelLoader, _moneyPosition, _loadingScreen, _trainUICurrentTrainView, Analytics, _camera);
            _globalMoneyView.Construct(_moneyView, _camera);
            _globalMapScroller.Construct(_camera);
        }
    }
}

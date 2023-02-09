using UnityEngine;

namespace Source.UI
{
    public class GlobalMoneyView : MonoBehaviour
    {
        [SerializeField] private CameraGlobalUISetter _cameraGlobalUI;

        private MoneyView _moneyView;

        public void Construct(MoneyView moneyView, Camera camera)
        {
            _moneyView = moneyView;
            _cameraGlobalUI.Init(camera);
        }
    }
}

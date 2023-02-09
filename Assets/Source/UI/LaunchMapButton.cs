using Source.LevelLoaderService;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI
{
    public class LaunchMapButton : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private ILevelLoader _levelLoader;

        public void Construct(ILevelLoader levelLoader)
        {
            _levelLoader = levelLoader;

            if (_button == null)
                return;

            _button.onClick.AddListener(LaunchMainMenu);
        }

        private void OnDestroy()
        {
            if (_button == null)
                return;

            _button.onClick.RemoveListener(LaunchMainMenu);
        }

        private void LaunchMainMenu()
        {
            _levelLoader.LoadMenu();
        }
    }
}

using Source.Characters;
using UnityEngine;

namespace Source
{
    public class DebugButtons : MonoBehaviour
    {
        private ClickHandler _clickHandler;

        private void Start()
        {
            _clickHandler = FindObjectOfType<ClickHandler>();
        }

        public void SetSpeed(float speed)
        {
            _clickHandler.enabled = false;
            Time.timeScale = speed;
        }
    }
}

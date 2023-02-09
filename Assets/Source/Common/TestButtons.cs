using Source.Characters;
using UnityEngine;

namespace Source.Common
{
    public class TestButtons : MonoBehaviour
    {
#if UNITY_EDITOR

        [SerializeField] private ClickHandler _clickHandler;

        private void Update()
        {
            float timeScale = Time.timeScale;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                Time.timeScale = 1;

            if (Input.GetKeyDown(KeyCode.Alpha2))
                Time.timeScale = 2;

            if (Input.GetKeyDown(KeyCode.Alpha3))
                Time.timeScale = 1.5f;

            if (Input.GetKeyDown(KeyCode.Alpha4))
                Time.timeScale = 4;

            if (Input.GetKeyDown(KeyCode.Alpha6))
                Time.timeScale = 6;

            if (Input.GetKeyDown(KeyCode.Alpha8))
                Time.timeScale = 8;

            if (Input.GetKeyDown(KeyCode.Alpha9))
                Time.timeScale = 12;

            if (timeScale != Time.timeScale)
                _clickHandler.enabled = false;
        }
#endif
    }
}
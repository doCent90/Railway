using UnityEngine;

namespace Source.UI
{
    public class OneTimeTutorial : MonoBehaviour
    {
        private const string TutorialKey = "Tutorial";

        [SerializeField] private string _tutorialName;

        protected virtual void OnEnable()
        {
            if (PlayerPrefs.GetInt(TutorialKey + _tutorialName) != 0)
                gameObject.SetActive(false);
        }

        public void Play()
        {
            gameObject.SetActive(true);
        }

        public void Complete()
        {
            PlayerPrefs.SetInt(TutorialKey + _tutorialName, 1);
            gameObject.SetActive(false);
        }
    }
}

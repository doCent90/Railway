using Source.UI;
using UnityEngine;

namespace Source.Map.InteractableObjects.Cells
{
    public class StationTutorial : ITutorial
    {
        private readonly ActionProgress _progress;
        private readonly GameObject _tutorial;

        public StationTutorial(ActionProgress progress, GameObject tutorial)
        {
            _tutorial = tutorial;
            _progress = progress;
        }

        public bool Completed => _progress.Completed;

        public void Stop() => _tutorial.gameObject.SetActive(false);
        public void StartTutorial() => _tutorial.gameObject.SetActive(true);
        public void Finish() => _tutorial.gameObject.SetActive(false);
    }
}

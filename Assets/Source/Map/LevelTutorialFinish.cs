using System;
using Source.Characters.Train;
using Source.SaveLoad;
using UnityEngine;

namespace Source.Map
{
    internal class LevelTutorialFinish : MonoBehaviour
    {
        [SerializeField] private GameObject _menuButton;

        private TrainCompleteMovement _trainMovement;
        private LevelData _levelData;

        public void Construct(TrainCompleteMovement trainMovement, LevelData levelData)
        {
            _trainMovement = trainMovement ? trainMovement : throw new ArgumentNullException();
            _levelData = levelData ?? throw new ArgumentNullException();
        }

        private void Update()
        {
            if (_levelData.CompleteLevelData.Claims > 0 || _trainMovement.FinishedMovement)
            {
                _menuButton.gameObject.SetActive(true);
                enabled = false;
            }
        }
    }
}

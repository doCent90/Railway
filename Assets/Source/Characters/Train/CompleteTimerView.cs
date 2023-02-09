using System;
using TMPro;
using UnityEngine;

namespace Source.Characters.Train
{
    internal class CompleteTimerView : MonoBehaviour, ICompleteTimerView
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        public void Show(TimeSpan time) => _textMeshPro.text = $"{time.Minutes}m {time.Seconds} s";
    }
}

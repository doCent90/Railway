using System.Globalization;
using Source.Characters.Upgrades;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class FloatLevelProgressionView : MonoBehaviour, IFloatLevelProgressionView
    {
        [SerializeField] private TextMeshProUGUI _textMeshPro;

        private UpgradeValue<float> _incomeValue;

        public void Construct(UpgradeValue<float> incomeValue) =>
            _incomeValue = incomeValue;

        private void Update()
        {
            _textMeshPro.text = _incomeValue.Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}

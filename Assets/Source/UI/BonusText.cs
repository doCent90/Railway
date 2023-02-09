using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class BonusText : MonoBehaviour
    {
        private const string AdditionalText = "+ ";

        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private Gradient _colorGradient;
        [SerializeField] private Vector2 _gradientBounds;

        public void SetAmount(int amount)
        {
            _textMeshPro.text = AdditionalText + amount;
            float lerpValue = Mathf.InverseLerp(_gradientBounds.x, _gradientBounds.y, amount);
            _textMeshPro.color = _colorGradient.Evaluate(lerpValue);
        }
    }
}
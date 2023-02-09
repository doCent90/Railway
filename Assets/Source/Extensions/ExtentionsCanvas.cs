using DG.Tweening;
using UnityEngine;

namespace Source.Extensions
{
    public static class Extentions
    {
        private static string[] _formatName = new[]
        {
            "", "K", "M", "T", "B", "S", "Q", "R", "X"
        };

        public const float Duration = 0.3f;
        public const float Delay = 0.5f;

        public static void EnableGroup(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(1f, duration);
        }

        public static void DelayedEnableGroup(this CanvasGroup canvasGroup, float duration = Duration, float delay = Delay)
        {
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(1f, duration).SetDelay(delay);
        }

        public static void DelayedDisableGroup(this CanvasGroup canvasGroup, float duration = Duration, float delay = Delay)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration).SetDelay(delay);
        }

        public static void DisableGroup(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration);
        }

        public static void DisableGroups(this CanvasGroup[] canvasGroups, float duration = Duration)
        {
            foreach (CanvasGroup canvas in canvasGroups)
                canvas.DisableGroup();
        }

        public static void EnableFade(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(1f, duration);
        }

        public static void DisableFade(this CanvasGroup canvasGroup, float duration = Duration)
        {
            canvasGroup.DOComplete(true);
            canvasGroup.DOFade(0f, duration);
        }

        public static string FormatNumbers(float value)
        {
            if (value <= 0)
                return "0";

            int index;
            int devide = 1000;

            if (value < devide)
                return value.ToString("0");

            for (index = 0; index < _formatName.Length; index++)
            {
                if (value >= devide)
                    value /= devide;
                else
                    break;
            }

            return value.ToString("0.0") + _formatName[index];
        }
    }
}

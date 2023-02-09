using UnityEngine;
using DG.Tweening;


namespace Source.UI
{
    public class GlobalTrainAnimation
    {
        private const float PunchSclaFactor = 0.1f;
        private const float Duration = 1f;

        private readonly Transform _train;
        private Tween _tween;

        public GlobalTrainAnimation(Transform train) => _train = train;

        public void Play(float progress)
        {
            if (_tween == null && progress != 1)
                _tween = _train.DOPunchScale(Vector3.one * PunchSclaFactor, Duration, 0, 1).SetLoops(-1);
            else
                Stop(progress);
        }

        private void Stop(float progress)
        {
            if (_tween != null && progress == 1)
            {
                _tween.Complete();
                _tween.Kill();
                _tween = null;
            }
        }
    }
}

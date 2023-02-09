using UnityEngine;
using DG.Tweening;
using Source.Extensions;

namespace Source.UI
{
    public class RegionUnlockAnimator
    {
        private const float Duration03sec = 0.3f;
        private const float Duration05sec = 0.5f;
        private const float Duration1sec = 1f;
        private const float PunchLockFactor = 1.2f;

        private readonly Camera _camera;
        private readonly MapRoad _mapRoad;
        private readonly Transform _container;
        private readonly CanvasGroup _stationIcon;
        private readonly CanvasGroup _buildButton;
        private readonly SpriteRenderer _spriteRendererLock;
        private readonly SpriteRenderer _spriteRendererUnLock;
        private readonly SpriteRenderer _spriteRendererRegion;

        public RegionUnlockAnimator(Camera camera, SpriteRenderer spriteRegion, SpriteRenderer spriteLock, SpriteRenderer spriteUnLock, Transform container, CanvasGroup stationIcon, MapRoad mapRoad, CanvasGroup buildButton)
        {
            _camera = camera;
            _mapRoad = mapRoad;
            _container = container;
            _stationIcon = stationIcon;
            _buildButton = buildButton;
            _spriteRendererLock = spriteLock;
            _spriteRendererUnLock = spriteUnLock;
            _spriteRendererRegion = spriteRegion;
        }

        public void Animate(string key)
        {
            PlayerPrefs.SetString(key, key);
            _stationIcon.DisableFade(0);
            _buildButton.DisableFade(0);
            _mapRoad.LineRenderer.transform.localScale = Vector3.zero;
            var newPosition = new Vector3(_container.position.x, _camera.transform.position.y, _container.position.z);

            Sequence sequence = DOTween.Sequence();
            sequence.Append(_camera.transform.DOMove(newPosition, Duration1sec));
            sequence.Append(_spriteRendererRegion.DOFade(0, Duration05sec));
            sequence.Append(_container.DOPunchScale(Vector3.one * PunchLockFactor, Duration1sec, 0, 0));
            sequence.Join(_spriteRendererLock.DOFade(0, Duration05sec));
            sequence.Join(_spriteRendererUnLock.DOFade(1, Duration05sec));
            sequence.Append(_spriteRendererUnLock.DOFade(0, Duration1sec));
            sequence.Append(_mapRoad.LineRenderer.transform.DOScale(Vector3.one, Duration05sec / 2));
            sequence.Append(_stationIcon.transform.DOPunchScale(Vector3.one * PunchLockFactor, Duration03sec, 0, 0));
            sequence.Join(_stationIcon.DOFade(1, Duration03sec / 2));
            sequence.Append(_buildButton.transform.DOPunchScale(Vector3.one * PunchLockFactor, Duration03sec, 0, 0).SetDelay(Duration03sec));
            sequence.Join(_buildButton.DOFade(1, Duration03sec / 2));
        }
    }
}

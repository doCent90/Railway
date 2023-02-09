using UnityEngine;

namespace Source.Characters.Worker
{
    public class SpeedEffect : MonoBehaviour
    {
        private const float DelayTime = 1.5f;

        [SerializeField] private Transform _model;
        [SerializeField] private ParticleSystem _smokeTrail;
        [SerializeField] private ParticleSystem _accelerateFx;
        [SerializeField] private ParticleSystem _accelerateFxExplosion;
        [SerializeField] private TrailRenderer[] _trailRenderers;

        private bool _isAccelerateFxExplosion = true;

        private void Start() => Invoke(nameof(Delay), DelayTime);

        private void Update()
        {
            foreach (TrailRenderer item in _trailRenderers)
                item.time = (Time.timeScale - 1) / 2f;

            bool accelerationIsActive = Time.timeScale > 1;
            _smokeTrail.gameObject.SetActive(accelerationIsActive);
            PlayAccelerateFx(accelerationIsActive);
        }

        private void PlayAccelerateFx(bool acceleration)
        {
            if (acceleration == false)
                _isAccelerateFxExplosion = false;

            if (acceleration && _accelerateFxExplosion.isStopped && _isAccelerateFxExplosion == false)
            {
                _accelerateFxExplosion.Play();
                _isAccelerateFxExplosion = true;
            }

            if (acceleration)
                _accelerateFx.Play();
            else
                _accelerateFx.Stop();
        }

        private void Delay() => _isAccelerateFxExplosion = false;
    }
}

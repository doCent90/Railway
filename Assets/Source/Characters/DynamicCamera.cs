using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Source.Characters.Train;
using UnityEngine;

namespace Source.Characters
{
    public class DynamicCamera : MonoBehaviour
    {
        private static readonly int ShowMergeHash = Animator.StringToHash("ShowMerge");
        private static readonly int ShowTrainHash = Animator.StringToHash("ShowTrain");
        private static readonly int ShowTargetHash = Animator.StringToHash("ShowBuilding");

        [SerializeField] private Animator _cameraAnimator;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private CinemachineVirtualCamera _showTargetCamera;
        [SerializeField] private CinemachineStateDrivenCamera _stateDrivenCamera;
        [SerializeField] private float _maxDistanceFactor;
        [SerializeField] private float _speed;

        private TrainMovement _trainMovement;
        private float _startZ;
        private float _startOrthoSize;
        private Coroutine _animation;

        public void Construct(TrainMovement trainMovement)
        {
            _stateDrivenCamera.m_Follow = trainMovement.transform;
            _stateDrivenCamera.m_LookAt = trainMovement.transform;
            _trainMovement = trainMovement;
        }

        private void Start() =>
            _startOrthoSize = _camera.m_Lens.OrthographicSize;

        private void LateUpdate() =>
            Zoom();

        private void Zoom()
        {
            float targetSize = Mathf.Lerp(_startOrthoSize, _startOrthoSize * _maxDistanceFactor,
                _trainMovement.CommonSpeed);

            if (_animation != null)
                StopCoroutine(_animation);

            _animation = StartCoroutine(AnimateCamera(targetSize, _speed));
        }

        public void ShowMerge(float duration)
        {
            if (_animation != null)
                StartCoroutine(Merge(duration));
        }

        public void ShowTrain() =>
            _cameraAnimator.SetTrigger(ShowTrainHash);

        public void Show(Transform transform)
        {
            _showTargetCamera.Follow = transform;
            _showTargetCamera.LookAt = transform;
            _cameraAnimator.SetTrigger(ShowTargetHash);
        }

        private IEnumerator Merge(float duration)
        {
            enabled = false;
            _cameraAnimator.SetTrigger(ShowMergeHash);
            yield return new WaitForSeconds(duration);
            ShowTrain();
            enabled = true;
        }

        private IEnumerator AnimateCamera(float target, float speed)
        {
            while (Math.Abs(_camera.m_Lens.OrthographicSize - target) > float.Epsilon)
            {
                _camera.m_Lens.OrthographicSize = Mathf.Lerp(_camera.m_Lens.OrthographicSize,
                    target, speed * Time.deltaTime);

                yield return null;
            }
        }
    }
}

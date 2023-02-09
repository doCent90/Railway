using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Source.Stack;
using UnityEngine;

namespace Source.Stack.View
{
    public abstract class StackView : MonoBehaviour, IStackableContainer
    {
        [SerializeField] private Transform _stackContainer;
        [SerializeField] private float _animationDuration;
        [SerializeField] private FloatSetting _scalePunch = new FloatSetting(true, 1.1f);
        [SerializeField] private FloatSetting _jumpPower = new FloatSetting(false, 0f);
        [SerializeField] private float _addDelay;

        private List<Transform> _transforms = new List<Transform>();
        private Coroutine _add;

        public event Action Removed;

        public void AddWithAnimation(Stackable stackable)
        {
            _transforms.Add(stackable.transform);
            _add = StartCoroutine(AddDelayed(stackable, _add));
        }

        public void Warp(Stackable stackable)
        {
            _transforms.Add(stackable.transform);
            stackable.transform.parent = _stackContainer;

            Vector3 endPosition = CalculateAddEndPosition(_stackContainer, stackable.transform);
            Vector3 endRotation = CalculateEndRotation(_stackContainer, stackable.transform);

            stackable.transform.localPosition = endPosition;
            stackable.transform.localRotation = Quaternion.Euler(endRotation);
        }

        private IEnumerator AddDelayed(Stackable stackable, Coroutine coroutine)
        {
            yield return coroutine;
            yield return new WaitForSeconds(_addDelay);

            if (stackable == null || _transforms.Contains(stackable.transform) == false)
                yield break;

            stackable.transform.parent = _stackContainer;

            Vector3 endPosition = CalculateAddEndPosition(_stackContainer, stackable.transform);
            Vector3 endRotation = CalculateEndRotation(_stackContainer, stackable.transform);
            Vector3 defaultScale = stackable.transform.localScale;

            stackable.transform.DOLocalMove(endPosition, _animationDuration);
            stackable.transform.DOLocalRotate(endRotation, _animationDuration);

            if (_scalePunch.Enabled)
                stackable.transform.DOPunchScale(defaultScale * _scalePunch.Value, _animationDuration);
            if (_jumpPower.Enabled)
                stackable.transform.DOLocalJump(endPosition, _jumpPower.Value, 1, _animationDuration);
        }

        public void Remove(Stackable stackable)
        {
            stackable.transform.DOComplete(true);
            stackable.transform.parent = null;

            int removedIndex = _transforms.IndexOf(stackable.transform);
            _transforms.RemoveAt(removedIndex);
            OnRemove(stackable.transform);

            Sort(_transforms);

            Removed?.Invoke();
        }

        public float FindTopPositionY()
        {
            float topPositionY = 0f;

            foreach (var item in _transforms)
                if (item.localPosition.y > topPositionY)
                    topPositionY = item.localPosition.y;

            return topPositionY;
        }

        protected virtual void OnRemove(Transform stackable)
        {
        }

        protected virtual Vector3 CalculateEndRotation(Transform container, Transform stackable)
        {
            return Vector3.zero;
        }

        protected abstract Vector3 CalculateAddEndPosition(Transform container, Transform stackable);

        protected virtual void Sort(List<Transform> unsortedTransforms)
        {
        }

        [Serializable]
        public class Setting<T>
        {
            [SerializeField] private bool _enabled;
            [SerializeField] private T _value;

            public Setting(bool enabled, T value)
            {
                _enabled = enabled;
                _value = value;
            }

            public bool Enabled => _enabled;
            public T Value => _value;
        }

        [Serializable]
        public class FloatSetting : Setting<float>
        {
            public FloatSetting(bool enabled, float value)
                : base(enabled, value)
            {
            }
        }

        [Serializable]
        public class VectorSetting : Setting<Vector3>
        {
            public VectorSetting(bool enabled, Vector3 value)
                : base(enabled, value)
            {
            }
        }
    }

    public interface IStackableContainer
    {
        event Action Removed;

        float FindTopPositionY();
    }
}

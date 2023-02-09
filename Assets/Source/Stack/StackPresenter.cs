using System;
using System.Collections.Generic;
using System.Linq;
using Source.Stack.View;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Stack
{
    public class StackPresenter : MonoBehaviour
    {
        [SerializeField] private StackView _stackView;
        [SerializeField] private int _stackCapacity;
        [SerializeField] private List<StackableTypes> _allTypesThatCanBeAdded;
        [SerializeField] private int _multiplier;

        private StackStorage _stack;

        public IEnumerable<Stackable> Data => _stack.Data;
        public bool IsFull => _stack.Count == _stack.Capacity;
        public int Count => _stack.Count;
        public int Capacity => _stack.Capacity;
        public bool Empty => Count == 0;
        public int Multiplier => _multiplier;

        public void Awake()
        {
            _stack = new StackStorage(_stackCapacity, _allTypesThatCanBeAdded);
        }

        public bool CanAddToStack(StackableType stackableType)
        {
            return _stack.CanAdd(stackableType) && _stackView != null;
        }

        public void AddToStack(Stackable stackable)
        {
            if (CanAddToStack(stackable.Type) == false)
                throw new InvalidOperationException();

            _stack.Add(stackable);
            _stackView.AddWithAnimation(stackable);
        }

        public void LoadInStack(Stackable stackable)
        {
            if (CanAddToStack(stackable.Type) == false)
                throw new InvalidOperationException();

            _stack.Add(stackable);
            _stackView.Warp(stackable);
        }

        public bool CanRemoveFromStack(StackableType stackableType)
        {
            return _stack.Contains(stackableType);
        }

        public IEnumerable<Stackable> RemoveAll()
        {
            var data = _stack.Data.ToArray();
            foreach (var stackable in data)
                RemoveFromStack(stackable);

            return data;
        }

        public void RemoveFromStack(Stackable stackable)
        {
            _stack.Remove(stackable);
            _stackView.Remove(stackable);
        }

        public Stackable RemoveFromStack(StackableType stackableType)
        {
            if (CanRemoveFromStack(stackableType) == false)
                throw new InvalidOperationException();

            var lastStackable = _stack.FindLast(stackableType);

            _stack.Remove(lastStackable);
            _stackView.Remove(lastStackable);

            return lastStackable;
        }
    }
}

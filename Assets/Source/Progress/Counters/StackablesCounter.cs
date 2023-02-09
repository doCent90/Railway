using System;
using System.Collections.Generic;
using System.Linq;
using Source.Stack;
using UnityEngine;

namespace Source.Progress.Counters
{
    [Serializable]
    public class StackablesCounter
    {
        [SerializeField] private List<StackableCount> _collection = new();

        public void Add(StackableType type)
        {
            if (_collection.Any(count => count.Type == type))
                _collection.First(count => count.Type == type).Add(1);
            else
                _collection.Add(new StackableCount(type, 1));
        }

        [Serializable]
        internal class StackableCount
        {
            public StackableCount(StackableType type, int count)
            {
                Type = type;
                Count = count;
            }

            public int Count { get; private set; }

            public StackableType Type { get; }

            public void Add(int amount) =>
                Count += amount;
        }

        public int GetAmount(StackableType stackableType)
        {
            if(_collection.Any(count => count.Type == stackableType))
                return _collection.First(count => count.Type == stackableType).Count;
            
            return 0;
        }
    }
}
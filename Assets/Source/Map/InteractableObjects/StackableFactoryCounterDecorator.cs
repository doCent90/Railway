using System;
using Source.Progress.Counters;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    internal class StackableFactoryCounterDecorator : MonoBehaviour, ISingleTypeStackableFactory
    {
        [SerializeField] private MonoBehaviour _stackableFactoryBehaviour;

        private ISingleTypeStackableFactory _stackableFactory => (ISingleTypeStackableFactory) _stackableFactoryBehaviour;

        private StackablesCounter _stackablesCounter;

        public void Construct(StackablesCounter stackablesCounter)
        {
            _stackablesCounter = stackablesCounter ?? throw new ArgumentException();
        }

        public Stackable Create()
        {
            _stackablesCounter.Add(Type);

            return _stackableFactory.Create();
        }

        public StackableType Type => _stackableFactory.Type;
    }
}
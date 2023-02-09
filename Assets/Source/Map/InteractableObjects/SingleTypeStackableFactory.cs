using System.Collections.Generic;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    internal class SingleTypeStackableFactory : MonoBehaviour, ISingleTypeStackableFactory
    {
        [SerializeField] private StackableType _type;
        [SerializeField] private List<Stackable> _stackableList;

        public StackableType Type => _type;

        private void OnValidate()
        {
            if (_stackableList == null)
                return;

            for (var i = 0; i < _stackableList.Count; i++)
                if (_stackableList[i]?.Type != _type)
                    _stackableList[i] = null;
        }

        public Stackable Create() =>
            Instantiate(GetStackable(), transform);

        private Stackable GetStackable() =>
            _stackableList[Random.Range(0, _stackableList.Count)];
    }
}
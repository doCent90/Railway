using System.Collections.Generic;
using Source.Stack;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    internal class RandomStackableFactory : MonoBehaviour, IStackableFactory
    {
        [SerializeField] private List<Stackable> _stackableList;

        public Stackable Create()
        {
            return Instantiate(_stackableList[Random.Range(0, _stackableList.Count)], transform);
        }
    }
}

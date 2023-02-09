using System.Collections.Generic;
using Source.Characters.Behaviour.Interactable;
using Source.Stack;
using UnityEngine;

namespace Source.Characters.Train.TrainCar
{
    public class TrainCar : MonoBehaviour
    {
        [SerializeField] private StackPresenter _stackPresenter;
        [SerializeField] private StackFulfillInteractable _stackFulfillInteractable;
        [SerializeField] private float _width;

        public ICharacterInteractable Interactable => _stackFulfillInteractable;
        public int ResourceCount => _stackPresenter.Count;
        public int Capacity => _stackPresenter.Capacity;
        public int CanAddAmount => _stackPresenter.Capacity - _stackPresenter.Count;
        public float Width => _width;

        public void Merge(TrainCar trainCar)
        {
            foreach (Stackable stackable in trainCar._stackPresenter.RemoveAll())
                _stackPresenter.LoadInStack(stackable);
        }

        public void Add(Stackable stackable)
        {
            _stackPresenter.LoadInStack(stackable);
        }

        public IEnumerable<Stackable> RemoveAll() =>
            _stackPresenter.RemoveAll();
    }
}

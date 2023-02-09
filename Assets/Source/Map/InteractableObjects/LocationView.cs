using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

namespace Source.Map.InteractableObjects
{
    internal class LocationView : MonoBehaviour, ILocationView
    {
        [SerializeField] private List<LocationObject> _list;
        [SerializeField] private LocationType _type;
        [SerializeField] private ParticleSystem _hit;
        [SerializeField] private ParticleSystem _particleSystem;

        public void SetLocation(LocationType type) =>
            _type = type;

        private void Start()
        {
            foreach (LocationObject locationObject in _list)
                locationObject.GameObject.SetActive(false);

            foreach (LocationObject locationObject in _list)
            {
                if (locationObject.LocationType == _type)
                    locationObject.GameObject.SetActive(true);
            }
        }

        public void ViewComplete()
        {
            if (_particleSystem != null)
                _particleSystem.Play();

            View(1);
        }

        private void View(float ratio) =>
            _list.First(o => o.LocationType == _type).GameObject.transform.DOScale(Vector3.one * (1 - ratio), 0.1f);

        public void Hit()
        {
            transform.DOShakeScale(0.3f, -0.15f, 1);
            transform.DOShakeRotation(0.3f, 20, 1);

            _hit.Play();
        }
    }

    [Serializable]
    internal class LocationObject
    {
        public LocationType LocationType;
        public GameObject GameObject;
    }
}

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Stack.View
{
    public class TransformListStackHolder : StackView
    {
        [SerializeField] private List<Transform> _placePoints;
    
        private readonly Dictionary<Transform, Transform> _placedObjects = new();

        protected override Vector3 CalculateAddEndPosition(Transform container, Transform stackable) => 
            GetNextFreePlace().localPosition;

        protected override Vector3 CalculateEndRotation(Transform container, Transform stackable)
        {
            Transform placeTransform = GetNextFreePlace();
            _placedObjects.Add(placeTransform, stackable);

            return placeTransform.localRotation.eulerAngles;
        }

        private Transform GetNextFreePlace() => 
            _placePoints.FirstOrDefault(transform => _placedObjects.ContainsKey(transform) == false);

        protected override void OnRemove(Transform stackable) => 
            _placedObjects.Remove(_placedObjects.First(pair => pair.Value == stackable).Key);
    }
}
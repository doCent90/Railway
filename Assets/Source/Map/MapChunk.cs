using System.Collections.Generic;
using Source.Map.InteractableObjects;
using Source.Map.InteractableObjects.Cells;
using UnityEngine;

namespace Source.Map
{
    public class MapChunk : MonoBehaviour
    {
        [SerializeField] private float _width;
        [SerializeField] private List<Cell> cells;
        [SerializeField] private List<Resource> _resources;

        public float Width => _width;

        public List<Cell> Cells => cells;
        public IEnumerable<Resource> Resources => _resources;
    }
}

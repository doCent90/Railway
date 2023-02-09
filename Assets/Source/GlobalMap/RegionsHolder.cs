using System.Collections.Generic;
using UnityEngine;
namespace Source.GlobalMap
{
    public class RegionsHolder : MonoBehaviour
    {
        [SerializeField] private Region[] _regions;

        public IReadOnlyList<Region> Regions => _regions;
    }
}

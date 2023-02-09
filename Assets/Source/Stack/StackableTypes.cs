using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Stack
{
    [CreateAssetMenu(fileName = "New Stackable Types", menuName = "Stackable/Types", order = 65)]
    public class StackableTypes : ScriptableObject, IStackableTypes
    {
        [SerializeField] private List<StackableType> _stackableTypes;

        public IReadOnlyList<StackableType> Value => _stackableTypes;

        public bool Contains(StackableType type)
        {
            return _stackableTypes.Contains(type);
        }

        public static StackableTypes All()
        {
            StackableTypes stackableTypes = CreateInstance<StackableTypes>();

            stackableTypes._stackableTypes =
                new List<StackableType>(Enum.GetValues(typeof(StackableType)).Cast<StackableType>());

            return stackableTypes;
        }
    }
}

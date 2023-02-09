using UnityEngine;

namespace Source.Stack
{
    class BaseStackable : Stackable
    {
        [SerializeField] private StackableType _type;
    
        public override StackableType Type => _type;
    }
}

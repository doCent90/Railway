using System;
using BehaviorDesigner.Runtime;
using Source.Map.InteractableObjects.Cells;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedCellWay : SharedVariable<CellWay>
    {
        public static implicit operator SharedCellWay(CellWay value) =>
            new SharedCellWay {Value = value};
    }
}
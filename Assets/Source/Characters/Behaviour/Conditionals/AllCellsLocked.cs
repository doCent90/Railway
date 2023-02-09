using System.Linq;
using BehaviorDesigner.Runtime.Tasks;
using Source.Characters.Behaviour.SharedVariables;

namespace Source.Characters.Behaviour.Conditionals
{
    public class AllCellsLocked : Conditional
    {
        public SharedCellWay SharedCell;

        public override TaskStatus OnUpdate()
        {
            bool allLocked = SharedCell.Value.Cells.All(cell => cell.Locked);
            allLocked &= SharedCell.Value.Cells.Count() != 0;
            
            TaskStatus taskStatus = allLocked ? TaskStatus.Success : TaskStatus.Failure;

            return taskStatus;
        }
    }
}
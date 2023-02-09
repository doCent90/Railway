using System.Collections.Generic;
using System.Linq;
using Source.Characters.Train;
using UnityEngine;

namespace Source.Map.InteractableObjects.Cells
{
    public class CellWay : IMovablePath
    {
        private readonly List<Cell> _cells = new List<Cell>();
        private Cell LastCompleted => _cells[GetLastIndex()];
        
        public IEnumerable<Cell> Cells => _cells;
        public Vector3 StopPoint => LastCompleted.transform.position;
        public Cell Last => _cells[^1];

        public void Add(Cell cell) =>
            _cells.Add(cell);

        public void Remove(Cell cell) =>
            _cells.Remove(cell);

        public bool CanMove(Vector3 transformPosition)
        {
            if (_cells.Count == 0)
                return false;

            return LastCompleted.transform.position.x < transformPosition.x;
        }

        public bool Finished(Vector3 position) => 
            _cells.All(cell => cell.Completed);

        public int GetNotFinishedAmount() => 
            Cells.Count(cell => cell.Completed == false);

        private int GetLastIndex()
        {
            Cell item = _cells.FirstOrDefault(cell => cell.Completed == false);
            int index = (item == null ? _cells.Count : _cells.IndexOf(item)) - 1;

            return index < 0 ? 0 : index;
        }
    }
}
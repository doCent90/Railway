using Source.Map.ChunksLoader;

namespace Source.Map.InteractableObjects.Cells
{
    class LockedRailBaseInteractable : RailBaseInteractable, ICellLock
    {
        public bool Locked => Progress.Completed == false;
    }
}

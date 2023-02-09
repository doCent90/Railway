namespace Source.Map.InteractableObjects.Cells
{
    internal class NullInteractCondition : IInteractCondition
    {
        public bool CanInteract() => 
            true;
    }
}
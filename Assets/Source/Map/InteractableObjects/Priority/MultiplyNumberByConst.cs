namespace Source.Map.InteractableObjects.Priority
{
    internal class MultiplyNumberByConst : IPriorityByNumber
    {
        private readonly int _multiplier;

        public MultiplyNumberByConst(int multiplier)
        {
            _multiplier = multiplier;
        }

        public int GetFor(int number) => 
            number * _multiplier;
    }
}
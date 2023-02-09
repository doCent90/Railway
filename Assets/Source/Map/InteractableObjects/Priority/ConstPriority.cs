using System;

namespace Source.Map.InteractableObjects.Priority
{
    internal class ConstPriority : IPriorityByNumber
    {
        private readonly int _constPriority;

        public ConstPriority(int constPriority)
        {
            if (constPriority < 0)
                throw new ArgumentException();
            
            _constPriority = constPriority;
        }

        public int GetFor(int number) => 
            _constPriority;
    }
}
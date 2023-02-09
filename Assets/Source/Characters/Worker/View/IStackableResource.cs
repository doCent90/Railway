using Source.Stack;

namespace Source.Characters.Worker.View
{
    public interface IStackableResource
    {
        StackableType StackableType { get; }
        bool WaitAddToStack { get; }
        void Hit();
    }
}
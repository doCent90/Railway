using Source.Stack;

namespace Source.Map.InteractableObjects
{
    public interface IStackableFactory
    {
        Stackable Create();
    }

    public interface ISingleTypeStackableFactory : IStackableFactory
    {
        StackableType Type { get; }
    }
}

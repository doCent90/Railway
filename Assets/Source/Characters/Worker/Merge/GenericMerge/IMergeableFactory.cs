namespace Source.Characters.Worker.Merge.GenericMerge
{
    public interface IMergeableFactory<in TMergeableConfig, out TMergeable> where TMergeableConfig : class
    {
        TMergeable Create(TMergeableConfig stats);
    }
}
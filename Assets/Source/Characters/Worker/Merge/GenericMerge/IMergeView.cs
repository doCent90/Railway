using System.Collections.Generic;

namespace Source.Characters.Worker.Merge.GenericMerge
{
    public interface IMergeView<in TMergeable>
    {
        void Merge(IEnumerable<TMergeable> mergeables, TMergeable newMerged);
    }
}

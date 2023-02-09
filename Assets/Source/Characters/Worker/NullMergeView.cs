using System.Collections.Generic;
using Source.Characters.Worker.Merge.GenericMerge;

namespace Source.Characters.Worker
{
    internal class NullMergeView : IMergeView<Worker>
    {
        public void Merge(IEnumerable<Worker> mergeables, Worker newMerged)
        {
        }
    }
}

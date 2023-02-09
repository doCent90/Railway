using System;

namespace Source.Characters.Worker.Merge.GenericMerge
{
    internal static class MergeHelper
    {
        public static float GetBaseLevelAmountForMergedLevel(int countToMerge, int level) =>
            (float) Math.Pow(countToMerge, level);

        public static int GetMergesCount(int baseAmount, int countToMerge)
        {
            if (baseAmount < 1)
                return 0;

            if (baseAmount == 1)
                return 1;

            return countToMerge * GetMergesCount(baseAmount - 1, countToMerge) + 1;
        }
    }
}
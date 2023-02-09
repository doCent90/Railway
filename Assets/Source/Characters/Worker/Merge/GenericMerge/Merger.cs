using System;
using System.Collections.Generic;
using System.Linq;

namespace Source.Characters.Worker.Merge.GenericMerge
{
    public class Merger<TMergeableConfig, TMergeable> where TMergeableConfig : class
    {
        private readonly Dictionary<TMergeableConfig, List<TMergeable>> _workers = new();
        private readonly IMergeableFactory<TMergeableConfig, TMergeable> _mergeableFactory;
        private readonly IMergeView<TMergeable> _mergeView;
        private readonly int _mergeLevels;
        private readonly int _countToMerge = 3;

        public Merger(IEnumerable<TMergeableConfig> configs,
            IMergeableFactory<TMergeableConfig, TMergeable> mergeableFactory,
            IMergeView<TMergeable> mergeView)
        {
            _mergeView = mergeView ?? throw new ArgumentException();
            _mergeLevels = configs.Count();
            _mergeableFactory = mergeableFactory ?? throw new ArgumentException();

            foreach (TMergeableConfig workerStat in configs)
                _workers.Add(workerStat, new List<TMergeable>());
        }

        public void SpawnStart(int baseAmount, int mergesLevel)
        {
            if(baseAmount < 0 || mergesLevel < 0)
                throw new ArgumentException();

            int merges = 0;
            int spawnedAmount = 0;

            for (int i = _mergeLevels - 1; i >= 0; i--)
            {
                float amountForMergedLevel = MergeHelper.GetBaseLevelAmountForMergedLevel(_countToMerge, i);

                for (int j = 0; j < (int) (baseAmount / amountForMergedLevel); j++)
                {
                    int mergesCount = MergeHelper.GetMergesCount(i, _countToMerge);

                    if (merges + mergesCount <= mergesLevel && spawnedAmount < baseAmount)
                    {
                        Spawn(i);
                        spawnedAmount += (int) amountForMergedLevel;
                        merges += mergesCount;
                    }
                }
            }
        }

        public void SpawnNew() =>
            Spawn(0);

        public bool CanMerge() =>
            _workers.Any(CanMerge);

        public void Merge()
        {
            if (CanMerge() == false)
                throw new InvalidOperationException();

            KeyValuePair<TMergeableConfig, List<TMergeable>> worker = _workers.First(CanMerge);
            Merge(worker.Key, worker.Value, _countToMerge);
        }

        private bool CanMerge(KeyValuePair<TMergeableConfig, List<TMergeable>> worker) =>
            worker.Value.Count >= _countToMerge && _workers.ElementAt(_workers.Count - 1).Key != worker.Key;

        private void Merge(TMergeableConfig workerStats, List<TMergeable> workerValue, int countToMerge)
        {
            List<TMergeable> workers = new();

            for (int i = 0; i < countToMerge; i++)
            {
                TMergeable worker = workerValue[0];
                workerValue.Remove(worker);
                workers.Add(worker);
            }

            TMergeable newWorker = default(TMergeable);

            for (int i = 0; i < _workers.Count; i++)
            {
                if (workerStats == _workers.ElementAt(i).Key)
                    newWorker = Spawn(_workers.ElementAt(i + 1).Key);
            }

            _mergeView.Merge(workers, newWorker);
        }

        private void Spawn(int index) =>
            Spawn(_workers.ElementAt(index).Key);

        private TMergeable Spawn(TMergeableConfig stats)
        {
            TMergeable worker = _mergeableFactory.Create(stats);
            _workers[stats].Add(worker);

            return worker;
        }
    }
}

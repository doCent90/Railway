using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Source.Characters.Worker.Merge.GenericMerge;
using Source.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Characters.Worker.Merge
{
    public class WorkerMergeView : IMergeView<Worker>
    {
        private readonly IHidableView _gameUI;
        private readonly DynamicCamera _camera;
        private readonly Transform _spawnPoint;
        private readonly ParticleSystem _particles;

        float _duration = 1f;

        public WorkerMergeView(IHidableView gameUI, DynamicCamera camera, ParticleSystem particles,
            Transform spawnPoint)
        {
            _spawnPoint = spawnPoint ? spawnPoint : throw new ArgumentException();
            _particles = particles ? particles : throw new ArgumentException();
            _camera = camera ? camera : throw new ArgumentException();
            _gameUI = gameUI ?? throw new ArgumentException();
        }

        public void Merge(IEnumerable<Worker> workers, Worker newWorker) =>
            _camera.StartCoroutine(ShowMerge(workers, newWorker));

        private IEnumerator ShowMerge(IEnumerable<Worker> workers, Worker newWorker)
        {
            _camera.ShowMerge(_duration);
            _gameUI.Hide();
            newWorker.gameObject.SetActive(false);

            foreach (Worker worker in workers)
                Merge(worker);

            yield return new WaitForSeconds(_duration);
            newWorker.gameObject.SetActive(true);
            ShowMergeSpawn(newWorker);

            _particles.Play();
            yield return new WaitForSeconds(1f);
            _gameUI.Show();
        }

        private void Merge(Worker worker)
        {
            worker.transform.SetParent(_spawnPoint.transform);

            worker.transform.DOLocalMove(Vector3.zero, _duration).SetEase(Ease.Linear)
                .OnComplete(() => { Object.Destroy(worker.gameObject); });

            if (Vector3.Distance(worker.transform.position, _spawnPoint.transform.position) > 1f)
                worker.transform.DOLookAt(_duration * _spawnPoint.transform.position - worker.transform.position,
                    _duration);
        }

        private void ShowMergeSpawn(Worker worker)
        {
            worker.transform.position = _spawnPoint.position;
            worker.transform.DOPunchScale(Vector3.one * 0.3f, 0.3f, 0, 0);
            worker.Merge();
        }
    }
}

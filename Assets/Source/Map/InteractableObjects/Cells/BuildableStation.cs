using System;
using System.Collections;
using System.Collections.Generic;
using Source.Characters;
using Source.Characters.Behaviour.Interactable;
using Source.Characters.Worker.View;
using Source.Map.ChunksLoader;
using Source.Money;
using Source.Stack;
using Source.Tutorials;
using Source.UI;
using UnityEngine;
using Random = System.Random;

namespace Source.Map.InteractableObjects.Cells
{
    public class BuildableStation : BuildableCell, ICharacterInteractable, IWorkSource, ICellLock
    {
        private const float EndLevelDelay = 3f;

        private static readonly int BuildTrigger = Animator.StringToHash("Build");

        [SerializeField] private BuildableCell _childBuildableCell;
        [SerializeField] private MonoBehaviour _interactConditionBehaviour;
        [SerializeField] private Animator _animator;
        [SerializeField] private Animation _particles;
        [SerializeField] private Transform[] _interactPoints;
        [SerializeField] private float _buildDuration = 2.5f;
        [SerializeField] private GameObject _tutorialObject;
        [SerializeField] private LevelCompleteScreen _levelCompleteScreen;

        private ITutorialRunner _tutorialRunner;
        private IInteractCondition _interactCondition;
        private float _lastInteraction;
        private Coroutine _building;
        private float _targetAnimationSpeed;
        private float _animationSpeedChange = 10f;
        private IHidableView _gameUI;
        private DynamicCamera _dynamicCamera;
        private ILevelFinish _buildingLevelFinish;
        private ITutorial _stationTutorial;
        private IEnvironmentPayer _environmentPayer;

        public bool Locked => true;

        public void Construct(ITutorialRunner tutorial, IHidableView gameUI, DynamicCamera dynamicCamera,
            ILevelFinish buildingLevelFinish, IEnvironmentPayer environmentPayer)
        {
            _environmentPayer = environmentPayer ?? throw new ArgumentNullException();
            _tutorialRunner = tutorial ?? throw new ArgumentNullException();
            _buildingLevelFinish = buildingLevelFinish;
            _dynamicCamera = dynamicCamera;
            _gameUI = gameUI;
            _stationTutorial = new StationTutorial(Progress, _tutorialObject);
            _levelCompleteScreen.Construct(_buildingLevelFinish, _environmentPayer, 100, _gameUI);
        }

        private void Awake()
        {
            _interactCondition = _interactConditionBehaviour
                ? (IInteractCondition)_interactConditionBehaviour
                : new NullInteractCondition();

            if (Progress.Completed)
            {
                _animator.SetTrigger(BuildTrigger);
                _animator.speed = 1;
                enabled = false;
            }
        }

        private void Update()
        {
            _animator.speed =
                Mathf.Lerp(_animator.speed, _targetAnimationSpeed, Time.deltaTime * _animationSpeedChange);
        }

        public bool CanInteract(StackPresenter enteredStack) =>
            CanInteract();

        public bool CanInteract(StackableType stackableType) =>
            CanInteract();

        private bool CanInteract() =>
            Progress.Completed == false && _interactCondition.CanInteract() && (_childBuildableCell == null ||
                _childBuildableCell.Progress.Completed);

        public void Interact(StackPresenter enteredStack, float deltaTime)
        {
            if (enteredStack.Count > 0)
            {
                foreach (Stackable stackable in enteredStack.RemoveAll())
                    Destroy(stackable.gameObject);
            }

            if (_building != null)
                return;

            _building = StartCoroutine(Build());
        }

        private IEnumerator Build()
        {
            if (_buildDuration > 0)
            {
                _tutorialRunner.Run(_stationTutorial);
                yield return new WaitForSeconds(1f);
                _animator.SetTrigger(BuildTrigger);
                _animator.speed = 0f;
                _targetAnimationSpeed = 0f;
                _gameUI.Hide();
                _dynamicCamera.Show(transform);
            }

            float time = 0;

            while (time < _buildDuration)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _particles.Play();
                    _targetAnimationSpeed = 1f;
                    yield return new WaitForSeconds(0.15f);
                    _targetAnimationSpeed = 0f;

                    time += 0.15f;
                }

                yield return null;
            }

            Progress.Complete();
            _particles.Stop();

            yield return new WaitForSeconds(EndLevelDelay);
            _levelCompleteScreen.Show();
        }

        public Vector3 GetInteractPoint(StackPresenter stackPresenter, Transform transformPosition)
        {
            Random random = new Random(transformPosition.gameObject.GetHashCode());
            Vector3 interactPosition = _interactPoints[random.Next(0, _interactPoints.Length)].position;

            return interactPosition +
                   (transformPosition.position - interactPosition).normalized * (random.Next() % 10 / 5f);
        }
    }
}

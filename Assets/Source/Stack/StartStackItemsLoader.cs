using UnityEngine;

namespace Source.Stack
{
    [RequireComponent(typeof(StackPresenter))]
    public class StartStackItemsLoader : MonoBehaviour
    {
        [SerializeField] private int _amount;
        [SerializeField] private Stackable _template;

        private StackPresenter _stackPresenter;

        private void Start() =>
            Load();

        private void Load()
        {
            _stackPresenter = GetComponent<StackPresenter>();

            for (int i = 0; i < _amount; i++)
            {
                Stackable stackable = Instantiate(_template, transform);
                _stackPresenter.AddToStack(stackable);
            }
        }
    }
}

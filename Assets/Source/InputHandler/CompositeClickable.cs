using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.InputHandler
{
    public class CompositeClickable : MonoBehaviour, IClickable
    {
        [SerializeField] private List<MonoBehaviour> _clickableBehaviours;

        private IEnumerable<IClickable> _clickables => _clickableBehaviours.Cast<IClickable>();
        
        private void OnValidate()
        {
            if (_clickableBehaviours == null) 
                return;
            
            for (var i = 0; i < _clickableBehaviours.Count; i++)
            {
                MonoBehaviour clickableBehaviour = _clickableBehaviours[i];

                if (clickableBehaviour is not IClickable)
                {
                    _clickableBehaviours[i] = null;
                        
                    Debug.LogWarning(clickableBehaviour.gameObject.name + "needs to implement" +
                                     nameof(IClickable));
                }
            }
        }

        public void Click()
        {
            foreach (IClickable clickable in _clickables) 
                clickable.Click();
        }
    }
}
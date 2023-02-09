using Source.Common;
using UnityEngine;

namespace Source.InputHandler
{
    public class ClickHandler : MonoBehaviour
    {
        [SerializeField] private RayCaster _rayCaster;

        private void Update()
        {
            if(Input.GetMouseButtonDown(0) == false)
                return;
            
            if (_rayCaster.TryRaycastComponent(out IClickable clickable, Input.mousePosition)) 
                clickable.Click();
        }
    }
}
using DG.Tweening;
using UnityEngine;

namespace Source.Stack
{
    public abstract class Stackable : MonoBehaviour
    {
        public abstract StackableType Type { get; }

        private void OnDestroy()
        {
            if (transform != null)
                transform.DOKill();
        }
    }
}

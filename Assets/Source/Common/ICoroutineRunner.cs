using System.Collections;
using UnityEngine;

namespace Source.Common
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}

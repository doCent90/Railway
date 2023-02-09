using UnityEngine;

namespace Source.Characters.Worker
{
    public class BotCharacterInput : MonoBehaviour, IInputSource
    {
        public Vector3 Destination { get; set; }
    }
}
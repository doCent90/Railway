using System;
using BehaviorDesigner.Runtime;
using Source.Characters.Worker;

namespace Source.Characters.Behaviour.SharedVariables
{
    [Serializable]
    public class SharedBotCharacterInput : SharedVariable<BotCharacterInput>
    {
        public static implicit operator SharedBotCharacterInput(BotCharacterInput value) =>
            new SharedBotCharacterInput {Value = value};
    }
}
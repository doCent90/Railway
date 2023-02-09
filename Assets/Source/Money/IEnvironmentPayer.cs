using UnityEngine;

namespace Source.Money
{
    public interface IEnvironmentPayer
    {
        void Pay(int amount, Vector3 transformPosition);
    }
}

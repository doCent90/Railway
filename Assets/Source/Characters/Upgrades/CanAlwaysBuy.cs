namespace Source.Characters.Upgrades
{
    public class CanAlwaysBuy : ICanBuyCondition
    {
        public bool CanBuy() => true;
    }
}

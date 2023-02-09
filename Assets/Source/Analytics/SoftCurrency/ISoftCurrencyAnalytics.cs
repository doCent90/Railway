namespace Source.Analytics.SoftCurrency
{
    public interface ISoftCurrencyAnalytics
    {
        void LogSoftSpent(string purchaseType, int amount);
        void LogSoftSpent(string purchaseType, string productType, int amount);
    }
}
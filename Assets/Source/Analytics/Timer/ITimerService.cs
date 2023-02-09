namespace Source.Analytics.Timer
{
    public interface ITimerService
    {
        float ElapsedTime { get; }
        void Reset();
    }
}
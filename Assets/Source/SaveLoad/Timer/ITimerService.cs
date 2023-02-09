namespace Source.SaveLoad.Timer
{
    public interface ITimerService
    {
        float ElapsedTime { get; }
        void Reset();
    }
}
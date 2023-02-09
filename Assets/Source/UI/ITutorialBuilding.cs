namespace Source.UI
{
    public interface ITutorial
    {
        bool Completed { get; }
        void Stop();
        void StartTutorial();
        void Finish();
    }
}

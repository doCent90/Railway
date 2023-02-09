namespace Source.Progress.Tasks
{
    public interface ITask
    {
        public float NormalizedProgress { get; }
        public float CurrentProgress { get; }
        public int TargetProgress { get; }
    }
}

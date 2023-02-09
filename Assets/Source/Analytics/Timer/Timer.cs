using System;

namespace Source.Analytics.Timer
{
    public class Timer : ITimerService
    {
        public float ElapsedTime { get; private set; }

        public void Reset() =>
            ElapsedTime = 0;

        public void Tick(float time)
        {
            if (time <= 0)
                throw new ArgumentException(nameof(time));
            
            ElapsedTime += time;
        }
    }
}
using System;

namespace Source.Map
{
    public class LevelTimer
    {
        private readonly DateTime _startTime;

        public LevelTimer()
        {
            _startTime = DateTime.Now;
        }

        public TimeSpan ElapsedTime => DateTime.Now - _startTime;
    }
}

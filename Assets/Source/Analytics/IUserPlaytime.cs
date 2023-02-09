using System;

namespace Source.Analytics
{
    public interface IUserPlaytime
    {
        TimeSpan AllPlayTime { get; }
    }
}
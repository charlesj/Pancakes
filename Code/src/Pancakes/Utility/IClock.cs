using System;

namespace Pancakes.Utility
{
    public interface IClock
    {
        DateTimeOffset Now();
    }

    public class Clock : IClock
    {
        public DateTimeOffset Now()
        {
            return DateTime.UtcNow;
        }
    }
}
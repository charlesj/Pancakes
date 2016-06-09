using System;

namespace Pancakes.Utility
{
    public class Clock : IClock
    {
        public DateTimeOffset Now()
        {
            return DateTime.UtcNow;
        }
    }
}
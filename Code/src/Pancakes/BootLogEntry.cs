using System;
using Pancakes.Utility;

namespace Pancakes
{
    public class BootLogEntry
    {
        public BootLogEntry(IClock clock, string level, string component, string message)
        {
            Level = level;
            Component = component;
            Message = message;
            this.Timestamp = clock.Now();
        }

        public DateTimeOffset Timestamp { get; }
        public string Level { get; }
        public string Component { get; }
        public string Message { get;  }
    }
}
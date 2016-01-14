using System;
using System.Collections.Generic;
using Pancakes.Utility;

namespace Pancakes
{
    public class BootLog
    {
        private readonly IClock clock;
        private List<BootLogEntry> log;
        private Action<string> output;

        public BootLog(IClock clock)
        {
            this.clock = clock;
            this.log = new List<BootLogEntry>();
        }

        public IReadOnlyList<BootLogEntry> Log => log;

        public void Info(string component, string message)
        {
            this.AddEntry("INFO", component, message);
        }

        public void Error(string component, string message)
        {
            this.AddEntry("ERROR", component, message);
        }

        private void AddEntry(string level, string component, string message)
        {
            var entry = new BootLogEntry(this.clock, level, component, message);
            this.log.Add(entry);
            this.output?.Invoke(
                $"{entry.Timestamp.LocalDateTime.ToString("HH:mm:ss")} {entry.Level} {entry.Component}: {entry.Message}");
        }

        public void SetOutstream(Action<string> output)
        {
            this.output = output;
        }
    }
}

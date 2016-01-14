using System.Collections.Generic;
using Pancakes.Utility;

namespace Pancakes
{
    public class Kernel
    {
        private BootLog bootlog;

        public BootConfiguration BootConfiguration { get; private set; }

        public IReadOnlyList<BootLogEntry> BootLog => this.bootlog.Log;

        public void Boot(BootConfiguration configuration)
        {
            this.BootConfiguration = configuration;
            this.bootlog = new BootLog(new Clock());
            if(BootConfiguration.Verbose)
                bootlog.SetOutstream(BootConfiguration.Output);

            this.bootlog.Info("Kernel", "Booting...");

            configuration.MarkAsBooted();

            this.bootlog.Info("Kernel", "Done");
        }
    }
}

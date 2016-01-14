using System.Collections.Generic;
using System.Linq;
using Pancakes.ServiceLocator;
using Pancakes.Utility;

namespace Pancakes
{
    public class Kernel
    {
        private BootLog bootlog;

        public BootConfiguration BootConfiguration { get; private set; }

        public IReadOnlyList<BootLogEntry> BootLog => this.bootlog.Log;

        public IServiceLocator ServiceLocator { get; private set; }

        public void Boot(BootConfiguration configuration)
        {
            this.BootConfiguration = configuration;
            this.bootlog = new BootLog(new Clock());
            if(BootConfiguration.Verbose)
                bootlog.SetOutstream(BootConfiguration.Output);

            this.bootlog.Info("Kernel", "Booting...");
            this.SetupServiceLocator(configuration.ServiceRegistrations, bootlog);

            configuration.MarkAsBooted();
            this.bootlog.Info("Kernel", "Done");
        }

        private void SetupServiceLocator(IReadOnlyList<IServiceRegistration> registrations, BootLog log)
        {
            this.bootlog.Info("ServiceLocator", "Loading Service Locator");
            var locator = new SimpleInjectorServiceLocator();
            locator.RegisterServices(registrations.ToArray(), log);
            this.ServiceLocator = locator;
            this.bootlog.Info("ServiceLocator", "Completed Loading");
        }
    }
}

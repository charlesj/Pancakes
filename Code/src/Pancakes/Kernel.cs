using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
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
            this.CheckSanity(ServiceLocator, configuration.SanityChecks.ToArray(), bootlog).GetAwaiter().GetResult();
            configuration.MarkAsBooted();
            this.bootlog.Info("Kernel", "Done");
        }

        private void SetupServiceLocator(IReadOnlyList<IServiceRegistration> registrations, BootLog log)
        {
            this.bootlog.Info(Constants.BootComponents.ServiceLocator, "Loading Service Locator");
            var locator = new SimpleInjectorServiceLocator();
            locator.RegisterServices(registrations.ToArray(), log);
            this.ServiceLocator = locator;
            this.bootlog.Info(Constants.BootComponents.ServiceLocator, "Completed Loading");
        }

        private async Task CheckSanity(IServiceLocator serviceLocator, Type[] sanityCheckTypes, BootLog log)
        {
            bootlog.Info(Constants.BootComponents.SanityChecks, "Starting Sanity Checks");
            var processor = serviceLocator.GetService<SanityCheckProcessor>();
            var sanity = await processor.Check(sanityCheckTypes, log);
            if(sanity.Any(kvp => kvp.Value == false))
                throw new BootException(CoreErrorCodes.InsaneKernel);
            bootlog.Info(Constants.BootComponents.SanityChecks, "Completed Sanity Checks");
        }
    }
}

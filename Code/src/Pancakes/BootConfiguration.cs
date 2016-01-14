using System;
using System.Collections.Generic;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.ServiceLocator;

namespace Pancakes
{
    public class BootConfiguration
    {
        private bool hasBeenBooted;
        private readonly List<IServiceRegistration> serviceRegistrations;

        public BootConfiguration()
        {
            this.serviceRegistrations = new List<IServiceRegistration>();
        }

        public bool Verbose { get; private set; }
        public Action<string> Output { get; private set; }

        public IReadOnlyList<IServiceRegistration> ServiceRegistrations => serviceRegistrations;

        public static BootConfiguration DefaultConfiguration
        {
            get { return new BootConfiguration(); }
        }

        public void MarkAsBooted()
        {
            this.hasBeenBooted = true;
        }

        public BootConfiguration BeVerbose()
        {
            this.ProtectAgainstConfiguringAfterBoot();
            this.Verbose = true;
            return this;
        }

        private void ProtectAgainstConfiguringAfterBoot()
        {
            if (this.hasBeenBooted)
                throw new ErrorCodeInvalidOperationException(CoreErrorCodes.CannotConfigurePostBoot);
        }

        public BootConfiguration WithOutput(Action<string> output)
        {
            this.Output = output;
            return this;
        }

        public BootConfiguration WithServices(IServiceRegistration serviceRegistration)
        {
            this.serviceRegistrations.Add(serviceRegistration);
            return this;
        }
    }
}

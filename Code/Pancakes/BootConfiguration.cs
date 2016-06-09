using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using Pancakes.Utility;

namespace Pancakes
{
    public class BootConfiguration
    {
        private bool hasBeenSealed;
        private List<IServiceRegistration> serviceRegistrations;
        private List<Type> sanityChecks;
        protected readonly AssemblyCollection assemblies;

        public BootConfiguration()
        {
            this.assemblies = new AssemblyCollection();
        }

        public bool Sealed => hasBeenSealed;

        public bool Verbose { get; private set; }
        public Action<string> Output { get; private set; }

        public IReadOnlyList<Assembly> Assemblies => assemblies;
        public IReadOnlyList<IServiceRegistration> ServiceRegistrations => serviceRegistrations;
        public IReadOnlyCollection<Type> SanityChecks => sanityChecks;

        public static BootConfiguration DefaultConfiguration
        {
            get { return new BootConfiguration(); }
        }


        public BootConfiguration Seal()
        {
            this.serviceRegistrations = this.assemblies
                        .GetTypesImplementing(typeof (IServiceRegistration))
                        .Select(t => (IServiceRegistration)Activator.CreateInstance(t))
                        .ToList();

            this.sanityChecks = this.assemblies.GetTypesImplementing(typeof (ICheckSanity)).ToList();

            this.hasBeenSealed = true;
            
            return this;
        }

        public BootConfiguration BeVerbose()
        {
            this.ProtectAgainstConfiguringAfterSealing();
            this.Verbose = true;
            return this;
        }

        public BootConfiguration WithOutput(Action<string> output)
        {
            ProtectAgainstConfiguringAfterSealing();   
            this.Output = output;
            return this;
        }

        public BootConfiguration LoadAssembly(Assembly assembly)
        {
            ProtectAgainstConfiguringAfterSealing();
            this.assemblies.Add(assembly);
            return this;
        }

        private void ProtectAgainstConfiguringAfterSealing()
        {
            if (this.hasBeenSealed)
                throw new ErrorCodeInvalidOperationException(CoreErrorCodes.CannotConfigurePostBoot);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Pancakes.Commands;
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
        private List<Type> commands;
        private bool suppressLoadingEntryPointAssembly;
        protected readonly AssemblyCollection assemblies;

        public BootConfiguration(AssemblyCollection assemblies = null)
        {
            if(assemblies == null)
                this.assemblies = new AssemblyCollection();
            else
                this.assemblies = assemblies;
        }

        public bool Sealed => hasBeenSealed;

        public bool Verbose { get; private set; }
        public Action<string> Output { get; private set; }

        public IReadOnlyList<Assembly> Assemblies => assemblies;
        public IReadOnlyList<IServiceRegistration> ServiceRegistrations => serviceRegistrations;
        public IReadOnlyList<Type> SanityChecks => sanityChecks;
        public IReadOnlyList<Type> Commands => commands;

        public static BootConfiguration DefaultConfiguration
        {
            get { return new BootConfiguration(); }
        }


        public BootConfiguration Seal()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            if (!this.suppressLoadingEntryPointAssembly &&
                !this.assemblies.Contains(entryAssembly))
                this.assemblies.Add(entryAssembly);

            var pancakesAssembly = typeof (BootConfiguration).GetTypeInfo().Assembly;
            if (!this.assemblies.Contains(pancakesAssembly))
                this.assemblies.Add(pancakesAssembly);

            this.serviceRegistrations = this.assemblies
                        .GetTypesImplementing(typeof (IServiceRegistration))
                        .Select(t => (IServiceRegistration)Activator.CreateInstance(t))
                        .ToList();

            this.sanityChecks = this.assemblies.GetTypesImplementing(typeof (ICheckSanity)).ToList();

            this.commands = this.assemblies.GetTypesImplementing(typeof(ICommand)).ToList();

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

        public BootConfiguration SuppressLoadingEntryPointAssembly()
        {
            ProtectAgainstConfiguringAfterSealing();
            this.suppressLoadingEntryPointAssembly = true;
            return this;
        }

        private void ProtectAgainstConfiguringAfterSealing()
        {
            if (this.hasBeenSealed)
                throw new ErrorCodeInvalidOperationException(CoreErrorCodes.CannotConfigurePostBoot);
        }
    }
}

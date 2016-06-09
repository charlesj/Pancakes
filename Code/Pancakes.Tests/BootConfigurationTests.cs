using System;
using System.Reflection;
using System.Threading.Tasks;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using SimpleInjector;
using Xunit;

namespace Pancakes.Tests
{
    public class BootConfigurationTests
    {
        [Fact]
        public void SetsVerbosity()
        {
            var config = new BootConfiguration();
            Assert.False(config.Verbose);
            config.BeVerbose();
            Assert.True(config.Verbose);
        }

        [Fact]
        public void CanSetOutput()
        {
            var written = string.Empty;
            var output = new Action<string>(s => written = s);
            var config = new BootConfiguration();

            config.WithOutput(output);
            config.Output("hello");
            Assert.Equal("hello", written);
        }

        [Fact]
        public void Sealing_MarksHasBeenSealed()
        {
            var config = new BootConfiguration();
            Assert.False(config.Sealed);
            config.Seal();
            Assert.True(config.Sealed);
        }

        [Fact]
        public void IntialState()
        {
            var config = new BootConfiguration();
            Assert.NotNull(config.Assemblies);
            Assert.Null(config.SanityChecks);
            Assert.Null(config.ServiceRegistrations);
        }

        [Fact]
        public void ServiceRegistrations_AvailableAfterSealing()
        {
            var config = new BootConfiguration();
            config.Seal();
            Assert.NotNull(config.ServiceRegistrations);
        }

        [Fact]
        public void CannotSetOutput_AfterBooting()
        {
            TestPostBootCheckWithAcion(config => config.WithOutput(str => { }));
        }

        [Fact]
        public void CannotConfigureAfterMarkingAsBooted()
        {
            TestPostBootCheckWithAcion(config => config.BeVerbose());
        }

        [Fact]
        public void CanAddAssembly()
        {
            var config = new BootConfiguration();

            var assembly = typeof (SanityCheck).GetTypeInfo().Assembly;
            config.LoadAssembly(assembly);

            Assert.Collection(config.Assemblies, item => Assert.Same(item, assembly));
        }

        [Fact]
        public void CannotAddAssembliesPostBoot()
        {
            var assembly = typeof(SanityCheck).GetTypeInfo().Assembly;
            TestPostBootCheckWithAcion(config => config.LoadAssembly(assembly));
        }

        [Fact]
        public void ServicesAreAvailable()
        {
            //Assert.Collection(config.ServiceRegistrations, item => Assert.Same(registration, item));
        }

        private void TestPostBootCheckWithAcion(Action<BootConfiguration> action)
        {
            var config = new BootConfiguration();
            config.Seal();
            var exception = Assert.Throws<ErrorCodeInvalidOperationException>(() => action(config)); 
            Assert.Equal(CoreErrorCodes.CannotConfigurePostBoot, exception.ErrorCode);
        }

        public class SanityCheck : ICheckSanity
        {
            public Task<bool> Probe()
            {
                return Task.FromResult(true);
            }
        }

        public class TestServiceRegistration : IServiceRegistration
        {
            public void RegisterServices(Container container)
            {
            }
        }
    }
}
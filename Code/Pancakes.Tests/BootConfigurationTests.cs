using System;
using System.Reflection;
using System.Threading.Tasks;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using SimpleInjector;
using Xunit;
using System.Linq;

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
            Assert.Null(config.Commands);
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
        public void ContainsEntryPointAssembly_ByDefault()
        {
            var config = new BootConfiguration();
            config.Seal();
            Assert.Collection(config.Assemblies, 
                item => Assert.True(item.FullName.StartsWith("dotnet-test-xunit")),
                item => Assert.NotNull(item)); // we don't care about this item for this test.
        }

        [Fact]
        public void DoesNotContainEntryPointAssembly_WhenSuppressed()
        {
            var config = new BootConfiguration();
            config.SuppressLoadingEntryPointAssembly().Seal();
            Assert.False(config.Assemblies.Any(ass => ass.FullName.StartsWith("dotnet-test-xunit")));
        }

        [Fact]
        public void ContainsPancakesAssembly_ByDefault()
        {
            var config = new BootConfiguration();
            config.Seal();
            Assert.Collection(config.Assemblies, item => Assert.NotNull(item), item => Assert.True(item.FullName.StartsWith("Pancakes")));
        }

        [Fact]
        public void CannotSuppressEntryPointAssemblyAfterSeal()
        {
            TestPostBootCheckWithAcion(config => config.SuppressLoadingEntryPointAssembly());
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
        public void PostSealing_ContainsValidServiceRegistrations()
        {
            var config = new BootConfiguration();
            config.LoadAssembly(typeof (TestServiceRegistration).GetTypeInfo().Assembly);
            config.Seal();

            Assert.True(config.ServiceRegistrations.Any(sr => sr.GetType() == typeof(TestServiceRegistration)));
        }

        [Fact]
        public void PostSealing_ContainsValidSanityChecks()
        {
            var config = new BootConfiguration();
            config.LoadAssembly(typeof (SanityCheck).GetTypeInfo().Assembly);
            config.Seal();

            Assert.True(config.SanityChecks.Any(s => s == typeof(SanityCheck)));
        }

        [Fact]
        public void PostSealing_ContainsValidCommands()
        {
            var config = new BootConfiguration();
            config.LoadAssembly(typeof (TestCommand).GetTypeInfo().Assembly);
            config.Seal();

            Assert.True(config.Commands.Any(s => s == typeof(TestCommand)));
        }


        [Fact]
        public void WillNotReaddAssemblies_AtSeal_WhenAlreadyAdded()
        {
            var config = new BootConfiguration();
            config.LoadAssembly(typeof(BootConfiguration).GetTypeInfo().Assembly);
            config.Seal();
            Assert.True(config.Assemblies.Count() == 2);
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

        public class TestCommand : Commands.ICommand
        {
            public Task<bool> AuthorizeAsync()
            {
                throw new NotImplementedException();
            }

            public Task ExecuteAsync()
            {
                throw new NotImplementedException();
            }

            public Task<bool> ValidateAsync()
            {
                throw new NotImplementedException();
            }
        }
    }
}
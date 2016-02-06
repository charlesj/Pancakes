using System;
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
        public void CanAddServiceRegistrations()
        {
            var config = new BootConfiguration();

            var registration = new TestServiceRegistration();
            config.WithServices(registration);

            Assert.Collection(config.ServiceRegistrations, item => Assert.Same(registration, item));
        }

        [Fact]
        public void CannotAddServiceRegistrationsPostBoot()
        {
            TestPostBootCheckWithAcion(config => config.WithServices(new TestServiceRegistration()));
        }

        [Fact]
        public void CanAddSanityChecks()
        {
            var config = new BootConfiguration();
            var check = new SanityCheck();
            config.CheckSanityWith(check);

            Assert.Collection(config.SanityChecks, item => Assert.Same(check, item));
        }

        [Fact]
        public void CannotAddSanityChecksPostBoot()
        {
            TestPostBootCheckWithAcion(config => config.CheckSanityWith(new SanityCheck()));
        }

        private void TestPostBootCheckWithAcion(Action<BootConfiguration> action)
        {
            var config = new BootConfiguration();
            config.MarkAsBooted();
            var exception = Assert.Throws<ErrorCodeInvalidOperationException>(() => action(config)); 
            Assert.Equal(CoreErrorCodes.CannotConfigurePostBoot, exception.ErrorCode);
        }

        public class SanityCheck : ICheckSanity
        {
            public bool Probe()
            {
                return true;
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
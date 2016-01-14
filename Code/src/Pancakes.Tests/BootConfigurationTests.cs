using System;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
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
        public void CannotConfigureAfterMarkingAsBooted()
        {
            var config = new BootConfiguration();
            config.MarkAsBooted();
            try
            {
                config.BeVerbose();
            }
            catch (ErrorCodeInvalidOperationException exception)
            {
                Assert.Equal(CoreErrorCodes.CannotConfigurePostBoot, exception.ErrorCode);
            }
        }

        [Fact]
        public void CanAddServiceRegistrations()
        {
            var config = new BootConfiguration();

            var registration = new TestServiceRegistration();
            config.WithServices(registration);

            Assert.Collection(config.ServiceRegistrations, item => Assert.Equal(registration, item));
        }

        public class TestServiceRegistration : IServiceRegistration
        {
            public void RegisterServices(Container container)
            {
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
using Xunit;

namespace Pancakes.Tests
{
    public class KernelTests
    {
        [Fact]
        public void AttemptingToBootNonSealedConfig_ThrowsException()
        {
            var kernel = new Kernel();
            var config = new BootConfiguration();
            var exception = Assert.Throws<BootException>(() => kernel.Boot(config));
            Assert.Equal(CoreErrorCodes.CannotBootNonSealedConfig, exception.ErrorCode);
        }

        [Fact]
        public void CallingBoot_SetsBootConfiguration()
        {
            var kernel = new Kernel();
            var configuration = new BootConfiguration().Seal();
            kernel.Boot(configuration);
            Assert.Equal(configuration, kernel.BootConfiguration);
        }

        [Fact]
        public void WritesToBootLog()
        {
            var kernel = new Kernel();
            var configuration = BootConfiguration.DefaultConfiguration.Seal();
            kernel.Boot(configuration);

            Assert.True(kernel.BootLog.Count > 0);
        }

        [Fact]
        public void IntegrationWithOutput()
        {
            var written = string.Empty;
            var output = new Action<string>(s => written = s);
            var kernel = new Kernel();
            var configuration = BootConfiguration.DefaultConfiguration
                .BeVerbose()
                .WithOutput(output)
                .Seal();

            kernel.Boot(configuration);

            Assert.True(!string.IsNullOrEmpty(written));
        }

        [Fact]
        public void ServiceLocatorIsSet_PostBoot()
        {
            var configuration = BootConfiguration.DefaultConfiguration.Seal();
            var kernel = new Kernel();
            kernel.Boot(configuration);

            Assert.NotNull(kernel.ServiceLocator);
        }

        [Fact]
        public void BadSanityCheckThrows()
        {
            // TODO: This test is awkward (and currently impossible)
            //var configuration = new BootConfiguration();
            ////configuration.CheckSanityWith(typeof (InsaneCheck));
            //var kernel = new Kernel();
            //var exception = Assert.Throws<BootException>(() => kernel.Boot(configuration));
            //Assert.Equal(CoreErrorCodes.InsaneKernel, exception.ErrorCode);
        }
    }
}

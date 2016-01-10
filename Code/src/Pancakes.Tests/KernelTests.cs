using System;
using Xunit;

namespace Pancakes.Tests
{
    public class KernelTests
    {
        [Fact]
        public void CallingBoot_SetsBootConfiguration()
        {
            var kernel = new Kernel();
            var configuration = new BootConfiguration();
            kernel.Boot(configuration);
            Assert.Equal(configuration, kernel.BootConfiguration);
        }

        [Fact]
        public void WritesToBootLog()
        {
            var kernel = new Kernel();
            var configuration = BootConfiguration.DefaultConfiguration;
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
                .WithOutput(output);

            kernel.Boot(configuration);

            Assert.True(!string.IsNullOrEmpty(written));
        }
    }
}

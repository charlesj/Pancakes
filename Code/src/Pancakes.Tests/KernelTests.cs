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
    }
}

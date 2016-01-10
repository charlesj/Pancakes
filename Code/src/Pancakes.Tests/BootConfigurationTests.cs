using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Xunit;

namespace Pancakes.Tests
{
    public class BootConfigurationTests
    {
        [Fact]
        public void SetsVerbosity()
        {
            var config = new BootConfiguration();
            Assert.False(config.BeVerbose);
            config.Verbose();
            Assert.True(config.BeVerbose);
        }

        [Fact]
        public void CannotConfigureAfterMarkingAsBooted()
        {
            var config = new BootConfiguration();
            config.MarkAsBooted();
            try
            {
                config.Verbose();
            }
            catch (ErrorCodeInvalidOperationException exception)
            {
                Assert.Equal(CoreErrorCodes.CannotConfigurePostBoot, exception.ErrorCode);
            }
        }
    }
}
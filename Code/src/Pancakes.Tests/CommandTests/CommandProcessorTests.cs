using System;
using Pancakes.Commands;
using Pancakes.ServiceLocator;
using Pancakes.Tests.TestUtilities;
using Xunit;

namespace Pancakes.Tests.CommandTests
{
    public class CommandProcessorTests : BaseUnitTest<CommandProcessor>
    {
        [Fact]
        public void ReturnsError_WhenCommandNameNull()
        {
            var result =  this.SystemUnderTest.Process("", "{}");
            Assert.Equal(CommandResultType.Error, result.ResultType);
        }

        [Fact]
        public void ReturnsError_WhenSerializetionIsNull()
        {
            var result = this.SystemUnderTest.Process("command", null);
            Assert.Equal(CommandResultType.Error, result.ResultType);
        }

        [Fact]
        public void ReturnsCommandNotFoundResult_WhenCommandNotRegistered()
        {
            this.Setup<ICommandRegistry>().Setup(reg => reg.IsRegistered("command")).Returns(false);
            var result = this.SystemUnderTest.Process("command", "{}");

            Assert.Equal(CommandResultType.Unknown, result.ResultType);
        }

        [Fact]
        public void ReturnsRerror_WhenCommandRegistryThrows()
        {
            this.Setup<ICommandRegistry>().Setup(reg => reg.IsRegistered("command")).Throws<Exception>();
            var result = this.SystemUnderTest.Process("command", "{}");

            Assert.Equal(CommandResultType.Error, result.ResultType);
        }

        [Fact]
        public void ReturnsError_WhenServiceLocatorError()
        {
            this.Setup<ICommandRegistry>().Setup(reg => reg.IsRegistered("command")).Returns(true);
            var commandType = this.GetMock<ICommand>().GetType();
            this.Setup<ICommandRegistry>()
                .Setup(reg => reg.Locate("command"))
                .Returns(commandType);
            this.Setup<IServiceLocator>().Setup(loc => loc.GetService(commandType)).Throws<Exception>();

            var result = this.SystemUnderTest.Process("command", "{}");

            Assert.Equal(CommandResultType.Error, result.ResultType);
        }
    }
}

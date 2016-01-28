﻿using Moq;
using Pancakes.Commands;
using Pancakes.Tests.TestUtilities;
using Xunit;

namespace Pancakes.Tests.CommandTests
{
    public class CommandExecutorTests : BaseUnitTest<CommandExecutor>
    {
        [Fact]
        public async void ReturnsInvalid_WhenValidateReturnsFalse()
        {
            this.Mock<ICommand>()
                .Setup(cmd => cmd.Validate()).Returns(false);

            var result = await this.SystemUnderTest.ExecuteAsync(this.GetMocked<ICommand>());
            
            Assert.Equal(CommandResultType.Invalid, result.ResultType);
        }
        
        [Fact]
        public async void ReturnsUnAuthorized_WhenAuthorizeReturnsFalse()
        {
            this.Mock<ICommand>()
               .Setup(cmd => cmd.Validate()).Returns(true);
            this.Mock<ICommand>()
                .Setup(cmd => cmd.Authorize()).Returns(false);

            var result = await this.SystemUnderTest.ExecuteAsync(this.GetMocked<ICommand>());

            Assert.Equal(CommandResultType.Unauthorized, result.ResultType);
        }

        [Fact]
        public async void ReturnsSuccess_WhenExecuteSuccess()
        {
            this.Mock<ICommand>()
              .Setup(cmd => cmd.Validate()).Returns(true);
            this.Mock<ICommand>()
                .Setup(cmd => cmd.Authorize()).Returns(true);

            var result = await this.SystemUnderTest.ExecuteAsync(this.GetMocked<ICommand>());

            Assert.Equal(CommandResultType.Success, result.ResultType);
            this.Mock<ICommand>()
                .Verify(cmd => cmd.Execute(), Times.Once);
        }
    }
}

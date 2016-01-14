using Pancakes.Commands;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.Tests.TestUtilities;
using Xunit;

namespace Pancakes.Tests.CommandTests
{
    public class CommandRegistryTests : BaseUnitTest<CommandRegistry>
    {
        public class BuildCommandName : CommandRegistryTests
        {
            [Fact]
            public void RemovesCommandFromTypeName()
            {
                var name = this.SystemUnderTest.BuildCommandName(typeof (TestCommand));
                Assert.Equal("test", name);
            }

            [Fact]
            public void ConvertsToLowerCase()
            {
                var name = this.SystemUnderTest.BuildCommandName(typeof(TestCommand));
                Assert.Equal("test", name);
                Assert.NotEqual("Test", name);
            }
        }

        public class IsRegistered : CommandRegistryTests
        {
            [Fact]
            public void ReturnsFalse_WhenNotRegistered()
            {
                Assert.False(this.SystemUnderTest.IsRegistered("Wokka"));
            }

            [Fact]
            public void ReturnsTrue_WhenPassedUppercase()
            {
                this.SystemUnderTest.Register(typeof(TestCommand));
                Assert.True(this.SystemUnderTest.IsRegistered("Test"));
            }

            [Fact]
            public void ReturnsTrue_WhenRegistered()
            {
                this.SystemUnderTest.Register(typeof(TestCommand));
                Assert.True(this.SystemUnderTest.IsRegistered("test"));
            }
        }

        public class Register : CommandRegistryTests
        {
            [Fact]
            public void WillNotRegister_NonCommands()
            {
                var exception = Assert.Throws<ErrorCodeInvalidOperationException>(() => this.SystemUnderTest.Register(typeof(string)));
                Assert.Equal(CoreErrorCodes.InvalidCommandRegistration, exception.ErrorCode);
            }

            [Fact]
            public void CanRegister_Commands()
            {
                this.SystemUnderTest.Register(typeof(TestCommand));
            }
        }

        public class GetRegisteredType : CommandRegistryTests
        {
            [Fact]
            public void Throws_NullException_WhenNullIsPassed()
            {
                var exception = Assert.Throws<ErrorCodeArgumentNullException>(
                    () => this.SystemUnderTest.GetRegisteredType(""));

                Assert.Equal(CoreErrorCodes.InvalidCommandLocationString, exception.ErrorCode);
            }

            [Fact]
            public void CanGet_RegisteredCommandType()
            {
                this.SystemUnderTest.Register(typeof(TestCommand));
                var type = this.SystemUnderTest.GetRegisteredType("test");

                Assert.Equal(typeof(TestCommand), type);
            }

            [Fact]
            public void CanGet_RegisteredCommandType_WithUpercaseCommand()
            {
                this.SystemUnderTest.Register(typeof(TestCommand));
                var type = this.SystemUnderTest.GetRegisteredType("Test");
                Assert.Equal(typeof(TestCommand), type);
            }
        }

        public class TestCommand : ICommand
        {
            public bool Authorize()
            {
                throw new System.NotImplementedException();
            }

            public bool Validate()
            {
                throw new System.NotImplementedException();
            }

            public void Execute()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}

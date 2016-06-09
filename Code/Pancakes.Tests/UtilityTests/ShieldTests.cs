using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.Utility;
using Xunit;

namespace Pancakes.Tests.UtilityTests
{
    public class ShieldTests
    {
        [Fact]
        public void SimpleNullCheck()
        {
            var exception = Assert.Throws<ErrorCodeArgumentNullException>(() => Shield.AgainstNull(null, "test"));
            Assert.Equal("test", exception.ParamName);
            Assert.Equal(CoreErrorCodes.ShieldCaughtNullObject, exception.ErrorCode);
        }

        [Fact]
        public void NonNullObjectPasses()
        {
            Shield.AgainstNull(new object(), "test");
        }

        [Theory]
        [InlineData((string)null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("             ")]
        [InlineData("\t\n")]
        public void StringNullCheck(string data)
        {
            var exception = Assert.Throws<ErrorCodeArgumentNullException>(() => Shield.AgainstNullString(data, "test"));
            Assert.Equal("test", exception.ParamName);
            Assert.Equal(CoreErrorCodes.ShieldCaughtBadString, exception.ErrorCode);
        }

        [Fact]
        public void GoodStringPasses()
        {
            Shield.AgainstNullString("good string", "test");
        }
    }
}

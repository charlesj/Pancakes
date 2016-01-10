using System;
using Pancakes.Tests.TestUtilities;
using Pancakes.Utility;
using Xunit;

namespace Pancakes.Tests
{
    public class BootLogTests : BaseUnitTest<BootLog>
    {
        public class Info : BootLogTests
        {
            [Fact]
            public void CanWriteToInfo()
            {
                var now = new DateTimeOffset(2015, 10, 30, 0,0,0, new TimeSpan());
                this.Setup<IClock>().Setup(clock => clock.Now()).Returns(now);
                this.SystemUnderTest.Info("test", "testing");
                Assert.Collection(this.SystemUnderTest.Log,
                    entry =>
                    {
                        Assert.Equal("INFO", entry.Level);
                        Assert.Equal("test", entry.Component);
                        Assert.Equal("testing", entry.Message);
                        Assert.Equal(now, entry.Timestamp);
                    });
            }

            [Fact]
            public void CanWriteToError()
            {
                var now = new DateTimeOffset(2015, 10, 30, 0, 0, 0, new TimeSpan());
                this.Setup<IClock>().Setup(clock => clock.Now()).Returns(now);
                this.SystemUnderTest.Error("test", "testing");
                Assert.Collection(this.SystemUnderTest.Log,
                    entry =>
                    {
                        Assert.Equal("ERROR", entry.Level);
                        Assert.Equal("test", entry.Component);
                        Assert.Equal("testing", entry.Message);
                        Assert.Equal(now, entry.Timestamp);
                    });
            }
        }

        public class SetOutstream : BootLogTests
        {
            [Fact]
            public void SetOutstream_WritesOut()
            {
                var written = string.Empty;
                var output = new Action<string>(s => written = s);
                this.SystemUnderTest.SetOutstream(output);

                Assert.Equal(0, this.SystemUnderTest.Log.Count);
                this.SystemUnderTest.Info("Test", "testing");
                Assert.True(!string.IsNullOrEmpty(written));
                Assert.True(written.Contains("testing"));
            }
        }
    }
}

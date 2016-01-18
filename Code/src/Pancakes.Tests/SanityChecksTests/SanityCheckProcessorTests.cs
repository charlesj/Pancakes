using System;
using Moq;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using Pancakes.Tests.TestUtilities;
using Xunit;

namespace Pancakes.Tests.SanityChecksTests
{
    public class SanityCheckProcessorTests : BaseUnitTest<SanityCheckProcessor>
    {
        [Fact]
        public void CanRunChecksWithEmptyList()
        {
            this.SystemUnderTest.Check(new Type[0]);
        }

        [Fact]
        public void RunChecksWithSingleTypeReturningTrue()
        {
            var sanityCheckType = typeof (GoodSanityCheck);
            this.Setup<IServiceLocator>()
                .Setup(sl => sl.GetService(sanityCheckType)).Returns(new GoodSanityCheck());
            Assert.True(this.SystemUnderTest.Check(new[] {sanityCheckType}));

            this.Setup<IServiceLocator>()
                .Verify(s => s.GetService(sanityCheckType), Times.Once);
        }

        [Fact]
        public void AnyFalsCheck_ReturnsFalse()
        {
            var badSanityCheckType = typeof(BadSanityCheck);
            this.Setup<IServiceLocator>()
                .Setup(sl => sl.GetService(badSanityCheckType)).Returns(new BadSanityCheck());
            var goodSanityCheckType = typeof(GoodSanityCheck);
            this.Setup<IServiceLocator>()
                .Setup(sl => sl.GetService(goodSanityCheckType)).Returns(new GoodSanityCheck());

            Assert.False(this.SystemUnderTest.Check(new[] { goodSanityCheckType, badSanityCheckType }));
        }

        public class GoodSanityCheck : ICheckSanity
        {
            public bool Probe()
            {
                return true;
            }
        }

        public class BadSanityCheck : ICheckSanity
        {
            public bool Probe()
            {
                return false;
            }
        }
    }
}
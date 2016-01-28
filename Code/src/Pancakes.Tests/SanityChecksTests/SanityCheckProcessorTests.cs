using System;
using System.Linq;
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
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(sanityCheckType)).Returns(new GoodSanityCheck());
            var sanityCheckResult = this.SystemUnderTest.Check(new[] {sanityCheckType});
            Assert.True(sanityCheckResult.All(kvp => kvp.Value));

            this.Mock<IServiceLocator>()
                .Verify(s => s.GetService(sanityCheckType), Times.Once);
        }

        [Fact]
        public void AnyFalseCheck_ReturnsFalse()
        {
            var badSanityCheckType = typeof(BadSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(badSanityCheckType)).Returns(new BadSanityCheck());
            var goodSanityCheckType = typeof(GoodSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(goodSanityCheckType)).Returns(new GoodSanityCheck());

            var result = this.SystemUnderTest.Check(new[] { goodSanityCheckType, badSanityCheckType });
            Assert.Collection(result, 
                                kvp => Assert.True(kvp.Value),
                                kvp => Assert.False(kvp.Value));
        }

        [Fact]
        public void NonConstructableSanityCheck_ReturnsFalse()
        {
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(It.IsAny<Type>())).Throws(new Exception());

            var result = this.SystemUnderTest.Check(new[] {typeof (BadSanityCheck)});
            Assert.Collection(result, kvp => Assert.False(kvp.Value));
        }

        [Fact]
        public void ProbeThrowingException_ReturnsFalse()
        {
            var failSanityCheck = typeof(FailSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(failSanityCheck)).Returns(new FailSanityCheck());

            var result = this.SystemUnderTest.Check(new[] { failSanityCheck });
            Assert.Collection(result, kvp => Assert.False(kvp.Value));
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

        public class FailSanityCheck : ICheckSanity
        {
            public bool Probe()
            {
                throw new NotImplementedException();
            }
        }
    }
}
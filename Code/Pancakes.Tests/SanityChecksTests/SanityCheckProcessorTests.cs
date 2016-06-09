using System;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using Pancakes.Tests.TestUtilities;
using Pancakes.Utility;
using Xunit;

namespace Pancakes.Tests.SanityChecksTests
{
    public class SanityCheckProcessorTests : BaseUnitTest<SanityCheckProcessor>
    {
        private readonly BootLog log;

        public SanityCheckProcessorTests()
        {
            log = new BootLog(GetMocked<IClock>());
        }

        [Fact]
        public void CanRunChecksWithEmptyList()
        {
            this.SystemUnderTest.Check(new Type[0], log);
        }

        [Fact]
        public async void RunChecksWithSingleTypeReturningTrue()
        {
            var sanityCheckType = typeof (GoodSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(sanityCheckType)).Returns(new GoodSanityCheck());
            var sanityCheckResult = await this.SystemUnderTest.Check(new[] {sanityCheckType}, log);
            Assert.True(sanityCheckResult.All(kvp => kvp.Value));

            this.Mock<IServiceLocator>()
                .Verify(s => s.GetService(sanityCheckType), Times.Once);
        }

        [Fact]
        public async void AnyFalseCheck_ReturnsFalse()
        {
            var badSanityCheckType = typeof(BadSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(badSanityCheckType)).Returns(new BadSanityCheck());
            var goodSanityCheckType = typeof(GoodSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(goodSanityCheckType)).Returns(new GoodSanityCheck());

            var result = await this.SystemUnderTest.Check(new[] { goodSanityCheckType, badSanityCheckType }, log);
            Assert.Collection(result, 
                                kvp => Assert.True(kvp.Value),
                                kvp => Assert.False(kvp.Value));
        }

        [Fact]
        public async void NonConstructableSanityCheck_ReturnsFalse()
        {
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(It.IsAny<Type>())).Throws(new Exception());

            var result = await this.SystemUnderTest.Check(new[] {typeof (BadSanityCheck)}, log);
            Assert.Collection(result, kvp => Assert.False(kvp.Value));
        }

        [Fact]
        public async void ProbeThrowingException_ReturnsFalse()
        {
            var failSanityCheck = typeof(FailSanityCheck);
            this.Mock<IServiceLocator>()
                .Setup(sl => sl.GetService(failSanityCheck)).Returns(new FailSanityCheck());

            var result = await this.SystemUnderTest.Check(new[] { failSanityCheck }, log);
            Assert.Collection(result, kvp => Assert.False(kvp.Value));
        }

        public class GoodSanityCheck : ICheckSanity
        {
            public Task<bool> Probe()
            {
                return Task.FromResult(true);
            }
        }

        public class BadSanityCheck : ICheckSanity
        {
            public Task<bool> Probe()
            {
                return Task.FromResult(false);
            }
        }

        public class FailSanityCheck : ICheckSanity
        {
            public Task<bool> Probe()
            {
                throw new NotImplementedException();
            }
        }
    }
}
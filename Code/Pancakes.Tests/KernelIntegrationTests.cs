using System;
using System.Threading.Tasks;
using Moq;
using Pancakes;
using Pancakes.Commands;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using Pancakes.Utility;
using Xunit;

public class KernelIntegrationTests
{
    [Fact]
    public void ThrowsBootException_WhenSanityCheckFails()
    {
        var kernel = new Kernel();
        var configuration = new BootConfiguration(AssemblyCollectionMockBuilder.GetWithFailingSanityCheck());
        configuration.Seal();
        var exception = Assert.Throws<BootException>(() => kernel.Boot(configuration));
        Assert.Equal(CoreErrorCodes.InsaneKernel, exception.ErrorCode);
    }

    public class AssemblyCollectionMockBuilder
    {
        public static AssemblyCollection GetWithFailingSanityCheck()
        {
            var mock = new Mock<AssemblyCollection>();
            SetupSanityCheckTypes(mock, typeof(InsaneSanityCheck));
            return mock.Object;
        }
        public static void SetupSanityCheckTypes(Mock<AssemblyCollection> mock, Type type)
        {
            mock.Setup(c => c.GetTypesImplementing(typeof(ICheckSanity))).Returns(new[] { type });          
        }

        public static void SetupServiceLocatorRegistrations(Mock<AssemblyCollection> mock, Type type)
        {
            mock.Setup(c => c.GetTypesImplementing(typeof(IServiceRegistration))).Returns(new[] { type });          
        }

        public static void SetupCommands(Mock<AssemblyCollection> mock, Type type)
        {
            mock.Setup(c => c.GetTypesImplementing(typeof(ICommand))).Returns(new[] { type });          
        }

        public class InsaneSanityCheck : ICheckSanity
        {
            public Task<bool> Probe()
            {
                throw new NotImplementedException();
            }
        }
    }
}
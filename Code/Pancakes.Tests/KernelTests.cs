using System;
using System.Threading.Tasks;
using Moq;
using Pancakes.Commands;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;
using Pancakes.SanityChecks;
using Pancakes.ServiceLocator;
using Pancakes.Utility;
using Xunit;
using Xunit.Abstractions;

namespace Pancakes.Tests
{
    public class KernelTests
    {
        private ITestOutputHelper output;

        public KernelTests(ITestOutputHelper output){
            this.output = output;
        }

        [Fact]
        public void AttemptingToBootNonSealedConfig_ThrowsException()
        {
            var kernel = new Kernel();
            var config = new BootConfiguration();
            var exception = Assert.Throws<BootException>(() => kernel.Boot(config));
            Assert.Equal(CoreErrorCodes.CannotBootNonSealedConfig, exception.ErrorCode);
        }

        [Fact]
        public void CallingBoot_SetsBootConfiguration()
        {
            var kernel = new Kernel();
            var configuration = new BootConfiguration().Seal();
            kernel.Boot(configuration);
            Assert.Equal(configuration, kernel.BootConfiguration);
        }

        [Fact]
        public void WritesToBootLog()
        {
            var kernel = new Kernel();
            var configuration = BootConfiguration.DefaultConfiguration.Seal();
            kernel.Boot(configuration);

            Assert.True(kernel.BootLog.Count > 0);
        }

        [Fact]
        public void IntegrationWithOutput()
        {
            var written = string.Empty;
            var output = new Action<string>(s => written = s);
            var kernel = new Kernel();
            var configuration = BootConfiguration.DefaultConfiguration
                .BeVerbose()
                .WithOutput(output)
                .Seal();

            kernel.Boot(configuration);

            Assert.True(!string.IsNullOrEmpty(written));
        }

        [Fact]
        public void ServiceLocatorIsSet_PostBoot()
        {
            var configuration = BootConfiguration.DefaultConfiguration.Seal();
            var kernel = new Kernel();
            kernel.Boot(configuration);

            Assert.NotNull(kernel.ServiceLocator);
        }

        [Fact]
        public void ThrowsBootException_WhenSanityCheckFails()
        {
            var kernel = new Kernel();
            var configuration = new BootConfiguration(AssemblyCollectionMockBuilder.GetWithFailingSanityCheck());
            configuration.Seal();
            var exception = Assert.Throws<BootException>(() => kernel.Boot(configuration));
            Assert.Equal(CoreErrorCodes.InsaneKernel, exception.ErrorCode);
        }

        [Fact]
        public void RegistersCommandsFromBootConfiguration()
        {
            var kernel = new Kernel();
            var configuration = new BootConfiguration(AssemblyCollectionMockBuilder.GetWithEasyCommand());
            configuration.WithOutput(this.output.WriteLine);
            configuration.Seal();

            kernel.Boot(configuration);

            var registry = kernel.ServiceLocator.GetService<ICommandRegistry>();
            Assert.True(registry.IsRegistered("Test"));
        }

        public class AssemblyCollectionMockBuilder
        {
            public static AssemblyCollection GetWithFailingSanityCheck()
            {
                var mock = new Mock<AssemblyCollection>();
                SetupServiceLocatorRegistrations(mock, typeof(Commands.DefaultCommandsServiceRegistration));
                SetupSanityCheckTypes(mock, typeof(InsaneSanityCheck));
                return mock.Object;
            }

            public static AssemblyCollection GetWithEasyCommand()
            {
                var mock = new Mock<AssemblyCollection>();
                SetupServiceLocatorRegistrations(mock, typeof(Commands.DefaultCommandsServiceRegistration));
                SetupCommands(mock, typeof(TestCommand));
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

            public class TestCommand : ICommand
            {
                public string Name { get; set; }

                public Task<bool> AuthorizeAsync()
                {
                    return Task.FromResult(true);
                }

                public Task<bool> ValidateAsync()
                {
                    return Task.FromResult(true);
                }

                public Task<object> ExecuteAsync()
                {
                    return Task.FromResult(new object());
                }
            }
        }
    }
}

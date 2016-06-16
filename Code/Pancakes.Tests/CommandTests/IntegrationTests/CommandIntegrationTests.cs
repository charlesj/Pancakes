using Pancakes.Commands;
using Pancakes.ServiceLocator;
using SimpleInjector;
using System.Threading.Tasks;
using Xunit;

namespace Pancakes.Tests.CommandTests.IntegrationTest
{
    public class CommandIntegrationTests
    {
        [Fact]
        public void FullRun()
        {
            var serviceLocator = new SimpleInjectorServiceLocator();
            serviceLocator.RegisterServices(new IServiceRegistration[] {new DefaultCommandsServiceRegistration(), new TestServiceRegistration() });

            var commandRegistry = serviceLocator.GetService<ICommandRegistry>();
            commandRegistry.Register(typeof(TestCommand));

            var processor = serviceLocator.GetService<ICommandProcessor>();
            var result = processor.Process("Test", "{\"Name\":\"Josh\"}");
            Assert.Equal(CommandResultType.Success, result.ResultType);
        }

        public class TestServiceRegistration : IServiceRegistration
        {
            public void RegisterServices(Container container)
            {
                container.Register<IFooService, FooService>();
            }
        }

        public interface IFooService
        {
            void Collect();
        }

        public class FooService : IFooService
        {
            public void Collect()
            {                
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

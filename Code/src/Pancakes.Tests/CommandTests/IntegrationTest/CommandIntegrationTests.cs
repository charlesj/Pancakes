using Pancakes.Commands;
using Pancakes.ServiceLocator;
using SimpleInjector;
using Xunit;

namespace Pancakes.Tests.CommandTests.IntegrationTest
{
    public class CommandIntegrationTests
    {
        [Fact]
        public void FullRun()
        {
            var container = new Container();
            container.Register<ICommandExecutor, CommandExecutor>(Lifestyle.Singleton);
            container.Register<ICommandRegistry, CommandRegistry>(Lifestyle.Singleton);
            container.Register<ICommandSerializer, CommandSerializer>(Lifestyle.Singleton);
            container.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Singleton);
            container.Register<IFooService, FooService>();
            var serviceLocator = new SimpleInjectorServiceLocator(container);
            container.Register<IServiceLocator>(() => serviceLocator, Lifestyle.Singleton);

            var commandRegistry = serviceLocator.GetService<ICommandRegistry>();
            commandRegistry.Register(typeof(TestCommand));

            var processor = serviceLocator.GetService<ICommandProcessor>();
            var result = processor.Process("Test", "{\"Name\":\"Josh\"}");
            Assert.Equal(CommandResultType.Success, result.ResultType);
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

        public class TestCommand :ICommand
        {
            public string Name { get; set; }

            public bool Authorize()
            {
                return true;
            }

            public bool Validate()
            {
                return true;
            }

            public void Execute()
            {
            }
        }
    }
}

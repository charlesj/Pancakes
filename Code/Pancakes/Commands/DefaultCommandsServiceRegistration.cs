using Pancakes.ServiceLocator;
using SimpleInjector;

namespace Pancakes.Commands
{
    public class DefaultCommandsServiceRegistration : IServiceRegistration
    {
        public void RegisterServices(Container container)
        {
            container.Register<ICommandExecutor, CommandExecutor>(Lifestyle.Singleton);
            container.Register<ICommandRegistry, CommandRegistry>(Lifestyle.Singleton);
            container.Register<ICommandSerializer, CommandSerializer>(Lifestyle.Singleton);
            container.Register<ICommandProcessor, CommandProcessor>(Lifestyle.Singleton);
        }
    }
}

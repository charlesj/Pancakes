using SimpleInjector;

namespace Pancakes.ServiceLocator
{
    public interface IServiceRegistration
    {
        void RegisterServices(Container container);
    }
}
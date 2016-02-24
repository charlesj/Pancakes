using System;
using SimpleInjector;

namespace Pancakes.ServiceLocator
{
    public class SimpleInjectorServiceLocator : IServiceLocator
    {
        public Container Container { get; private set; }

        public object GetService(Type type)
        {
            return Container.GetInstance(type);
        }

        public TServiceType GetService<TServiceType>() where TServiceType : class
        {
            return Container.GetInstance<TServiceType>();
        }

        public void RegisterServices(IServiceRegistration[] registrations)
        {
            this.RegisterServices(registrations, null);
        }

        internal void RegisterServices(IServiceRegistration[] registrations, BootLog log)
        {
            Container = new Container();
            foreach (var registration in registrations)
            {
                log?.Info("ServiceLocator", $"Loading Services {registration.GetType().Name}");
                registration.RegisterServices(Container);
            }

            Container.Register<IServiceLocator>(() => this, Lifestyle.Singleton);
        }
    }
}

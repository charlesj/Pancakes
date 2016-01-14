using System;
using SimpleInjector;

namespace Pancakes.ServiceLocator
{
    public class SimpleInjectorServiceLocator : IServiceLocator
    {
        private Container container;

        public object GetService(Type type)
        {
            return this.container.GetInstance(type);
        }

        public TServiceType GetService<TServiceType>() where TServiceType : class
        {
            return this.container.GetInstance<TServiceType>();
        }

        public void RegisterServices(IServiceRegistration[] registrations)
        {
            this.RegisterServices(registrations, null);
        }

        internal void RegisterServices(IServiceRegistration[] registrations, BootLog log)
        {
            this.container = new Container();
            foreach (var registration in registrations)
            {
                log?.Info("ServiceLocator", $"Loading Services {registration.GetType().Name}");
                registration.RegisterServices(container);
            }

            container.Register<IServiceLocator>(() => this, Lifestyle.Singleton);
        }
    }
}

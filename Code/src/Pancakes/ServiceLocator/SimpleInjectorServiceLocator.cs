using System;
using SimpleInjector;

namespace Pancakes.ServiceLocator
{
    public class SimpleInjectorServiceLocator : IServiceLocator
    {
        private readonly Container container;

        public SimpleInjectorServiceLocator(Container container)
        {
            this.container = container;
        }

        public object GetService(Type type)
        {
            return this.container.GetInstance(type);
        }

        public TServiceType GetService<TServiceType>() where TServiceType : class
        {
            return this.container.GetInstance<TServiceType>();
        }
    }
}

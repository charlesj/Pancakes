using System;

namespace Pancakes.ServiceLocator
{
    public interface IServiceLocator
    {
        object GetService(Type type);
        TServiceType GetService<TServiceType>() where TServiceType : class;
    }
}

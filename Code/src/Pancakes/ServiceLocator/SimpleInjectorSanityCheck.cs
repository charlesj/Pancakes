using System;
using Pancakes.SanityChecks;
using SimpleInjector;

namespace Pancakes.ServiceLocator
{
    public class SimpleInjectorSanityCheck : ICheckSanity
    {
        private readonly IServiceLocator _serviceLocator;

        public SimpleInjectorSanityCheck(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public bool Probe()
        {
            try
            {
                var Container = ((SimpleInjectorServiceLocator) _serviceLocator).Container;
                Container.Verify(VerificationOption.VerifyOnly);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

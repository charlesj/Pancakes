using System;
using System.Threading.Tasks;
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

        public Task<bool> Probe()
        {
            try
            {
                var Container = ((SimpleInjectorServiceLocator) _serviceLocator).Container;
                Container.Verify(VerificationOption.VerifyOnly);
                return Task.FromResult(true);
            }
            catch (Exception)
            {
                return Task.FromResult(false);
            }
        }
    }
}

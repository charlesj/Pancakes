using System;
using System.Linq;
using Pancakes.ServiceLocator;

namespace Pancakes.SanityChecks
{
    public class SanityCheckProcessor
    {
        private readonly IServiceLocator serviceLocator;

        public SanityCheckProcessor(IServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        public bool Check(Type[] types)
        {
            var results = types.Select(type =>
            {
                var check = (ICheckSanity) this.serviceLocator.GetService(type);
                return check.Probe();
            });

            return results.All(r => r);
        }
    }
}

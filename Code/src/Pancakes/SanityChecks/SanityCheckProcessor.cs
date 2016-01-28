using System;
using System.Collections.Generic;
using Pancakes.Extensions;
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

        public SanityCheckResult Check(Type[] types)
        {
            var result = new SanityCheckResult();
            types.Each(type =>
            {
                var check = (ICheckSanity)this.serviceLocator.GetService(type);
                result.Add(type, check.Probe());
            });

            return result;
        }
    }

    public class SanityCheckResult : Dictionary<Type, bool>
    {
        
    }
}

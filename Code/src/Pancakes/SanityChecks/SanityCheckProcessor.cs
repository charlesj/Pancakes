using System;
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

        public SanityCheckResult Check(Type[] types, BootLog log)
        {
            var result = new SanityCheckResult();
            types.Each(type =>
            {
                try
                {
                    var check = (ICheckSanity)serviceLocator.GetService(type);
                    var sane = check.Probe();
                    result.Add(type, sane);
                    if(sane)
                        log.Info(Constants.BootComponents.SanityChecks, $"{type.Name} passed.");
                    else
                        log.Error(Constants.BootComponents.SanityChecks, $"{type.Name} failed.");
                }
                catch (Exception)
                {
                    result.Add(type, false);
                    log.Error(Constants.BootComponents.SanityChecks, $"{type.Name} failed significantly.");
                }
            });

            return result;
        }
    }
}

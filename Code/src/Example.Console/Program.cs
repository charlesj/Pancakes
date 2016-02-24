using Example.Console.SanityChecks;
using Pancakes;
using Pancakes.ServiceLocator;

namespace Example.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bootConfig = BootConfiguration.DefaultConfiguration
                .BeVerbose()
                .WithOutput(System.Console.WriteLine)
                .CheckSanityWith(typeof (InternetAccessSanityCheck))
                .CheckSanityWith(typeof (SimpleInjectorSanityCheck));

            var kernel = new Kernel();
            kernel.Boot(bootConfig);

            System.Console.WriteLine("Boot complete");
            System.Console.ReadLine();
        }
    }
}

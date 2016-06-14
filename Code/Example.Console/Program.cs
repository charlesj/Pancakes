using Example.Console.SanityChecks;
using Pancakes;
using Pancakes.Extensions;
using Pancakes.Commands;
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
                .Seal();

            var kernel = new Kernel();
            kernel.Boot(bootConfig);
            System.Console.WriteLine("Boot complete");
            System.Console.WriteLine(bootConfig.Commands.ToJson());

            var processor = kernel.ServiceLocator.GetService<CommandProcessor>();
            var requestObj = new { Url = "http://www.google.com" };
            var request = requestObj.ToJson();
            var result = processor.Process("DownloadUrl", request);
            System.Console.WriteLine("Results:");
            System.Console.WriteLine(result.ToJson());
            System.Console.ReadLine();
        }
    }
}

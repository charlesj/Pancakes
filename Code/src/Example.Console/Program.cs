using Pancakes;

namespace Example.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bootConfig = BootConfiguration.DefaultConfiguration
                                .BeVerbose()
                                .WithOutput(s => System.Console.WriteLine(s));

            var kernel = new Kernel();
            kernel.Boot(bootConfig);
            System.Console.WriteLine("Boot complete");
            System.Console.ReadLine();
        }
    }
}

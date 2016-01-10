namespace Pancakes
{
    public class Kernel
    {
        public BootConfiguration BootConfiguration { get; private set; }

        public void Boot(BootConfiguration configuration)
        {
            this.BootConfiguration = configuration;

            configuration.MarkAsBooted();
        }
    }
}

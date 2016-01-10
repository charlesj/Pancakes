using System;
using Pancakes.ErrorCodes;
using Pancakes.Exceptions;

namespace Pancakes
{
    public class BootConfiguration
    {
        private bool hasBeenBooted;

        public BootConfiguration()
        {
        }

        public bool Verbose { get; private set; }

        public Action<string> Output { get; private set; }

        public static BootConfiguration DefaultConfiguration
        {
            get { return new BootConfiguration(); }
        }

        public void MarkAsBooted()
        {
            this.hasBeenBooted = true;
        }

        public BootConfiguration BeVerbose()
        {
            this.ProtectAgainstConfiguringAfterBoot();
            this.Verbose = true;
            return this;
        }

        private void ProtectAgainstConfiguringAfterBoot()
        {
            if (this.hasBeenBooted)
                throw new ErrorCodeInvalidOperationException(CoreErrorCodes.CannotConfigurePostBoot);
        }

        public BootConfiguration WithOutput(Action<string> output)
        {
            this.Output = output;
            return this;
        }
    }
}

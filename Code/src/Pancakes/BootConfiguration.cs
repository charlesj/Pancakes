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

        public bool BeVerbose { get; private set; }

        public static BootConfiguration DefaultConfiguration
        {
            get { return new BootConfiguration(); }
        }

        public void MarkAsBooted()
        {
            this.hasBeenBooted = true;
        }

        public BootConfiguration Verbose()
        {
            this.ProtectAgainstConfiguringAfterBoot();
            this.BeVerbose = true;
            return this;
        }

        private void ProtectAgainstConfiguringAfterBoot()
        {
            if (this.hasBeenBooted)
                throw new ErrorCodeInvalidOperationException(CoreErrorCodes.CannotConfigurePostBoot);
        }
    }
}

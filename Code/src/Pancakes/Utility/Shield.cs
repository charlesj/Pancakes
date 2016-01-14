using Pancakes.ErrorCodes;
using Pancakes.Exceptions;

namespace Pancakes.Utility
{
    public static class Shield
    {
        public static void AgainstNull(object obj, string argumentName)
        {
            if (obj == null)
                throw new ErrorCodeArgumentNullException(CoreErrorCodes.ShieldCaughtNullObject, argumentName);
        }

        public static void AgainstNullString(string data, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(data))
                throw new ErrorCodeArgumentNullException(CoreErrorCodes.ShieldCaughtBadString, argumentName);
        }
    }
}

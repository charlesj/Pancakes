using Pancakes.ErrorCodes;

namespace Pancakes.Exceptions
{
    interface IErrorCodeException
    {
        ErrorCode ErrorCode { get; }
    }
}

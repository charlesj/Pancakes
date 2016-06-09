using System;
using Pancakes.ErrorCodes;

namespace Pancakes.Exceptions
{
    public class BootException : Exception, IErrorCodeException
    {
        public BootException(ErrorCode errorCode) : base(errorCode.ToString())
        {
            ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; }
    }
}
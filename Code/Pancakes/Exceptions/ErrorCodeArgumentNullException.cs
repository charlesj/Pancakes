using System;
using Pancakes.ErrorCodes;

namespace Pancakes.Exceptions
{
    public class ErrorCodeArgumentNullException : ArgumentNullException, IErrorCodeException
    {
        private ErrorCodeArgumentNullException() { }

        public ErrorCodeArgumentNullException(ErrorCode errorCode, string paramName) : base(paramName)
        {
            this.ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; }
    }
}

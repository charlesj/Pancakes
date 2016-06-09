using System;
using Pancakes.ErrorCodes;

namespace Pancakes.Exceptions
{
    public class ErrorCodeInvalidOperationException : InvalidOperationException, IErrorCodeException
    {
        private ErrorCodeInvalidOperationException() { }

        public ErrorCodeInvalidOperationException(ErrorCode errorCode, Exception innerException) : base(errorCode.ToString(), innerException)
        {
            this.ErrorCode = errorCode;
        }

        public ErrorCodeInvalidOperationException(ErrorCode errorCode)
        {
            this.ErrorCode = errorCode;
        }

        public ErrorCode ErrorCode { get; private set; }
    }
}

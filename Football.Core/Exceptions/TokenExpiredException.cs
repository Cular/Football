using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Core.Exceptions
{
    public class TokenExpiredException : BaseException
    {
        public TokenExpiredException() : base()
        {
            this.BaseExceptionCode = BaseExceptionCode.TokenExpired;
        }

        public TokenExpiredException(string message) : base(message)
        {
            this.BaseExceptionCode = BaseExceptionCode.TokenExpired;
        }

        public TokenExpiredException(string message, Exception innerException) : base(message, innerException)
        {
            this.BaseExceptionCode = BaseExceptionCode.TokenExpired;
        }
    }
}

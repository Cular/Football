using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Exceptions
{
    public class TokenInactiveException : BaseException
    {
        public TokenInactiveException() : base()
        {
            this.BaseExceptionCode = BaseExceptionCode.TokenInactive;
        }

        public TokenInactiveException(string message) : base(message)
        {            
            this.BaseExceptionCode = BaseExceptionCode.TokenInactive;
        }

        public TokenInactiveException(string message, Exception innerException) : base(message, innerException)
        {   
            this.BaseExceptionCode = BaseExceptionCode.TokenInactive;
        }
    }
}

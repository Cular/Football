using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Exceptions
{
    public class ForbiddenException : BaseException
    {
        public ForbiddenException() : base()
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
            this.BaseExceptionCode = BaseExceptionCode.Forbidden;
        }

        public ForbiddenException(string message) : base(message)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
            this.BaseExceptionCode = BaseExceptionCode.Forbidden;
        }

        public ForbiddenException(string message, Exception innerException) : base(message, innerException)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Forbidden;
            this.BaseExceptionCode = BaseExceptionCode.Forbidden;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Football.Core.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException()
        {
            this.BaseExceptionCode = BaseExceptionCode.Undefined;
            this.HttpStatusCode = HttpStatusCode.BadRequest;
        }

        public BaseException(string message) : base(message)
        {
        }

        public BaseException(string message, Exception innerException) : base (message, innerException)
        { 
        }

        public BaseExceptionCode BaseExceptionCode { get; protected set; }

        public HttpStatusCode HttpStatusCode { get; protected set; }
    }
}

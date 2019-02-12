using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Exceptions
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base()
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
            this.BaseExceptionCode = BaseExceptionCode.NotFound;
        }

        public NotFoundException(string message) : base(message)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
            this.BaseExceptionCode = BaseExceptionCode.NotFound;
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.NotFound;
            this.BaseExceptionCode = BaseExceptionCode.NotFound;
        }
    }
}

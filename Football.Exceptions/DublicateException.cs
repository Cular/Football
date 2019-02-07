using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Exceptions
{
    public class DublicateException : BaseException
    {
        public DublicateException() : base()
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
            this.BaseExceptionCode = BaseExceptionCode.Dublicate;
        }

        public DublicateException(string message) : base(message)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
            this.BaseExceptionCode = BaseExceptionCode.Dublicate;
        }

        public DublicateException(string message, Exception innerException) : base(message, innerException)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
            this.BaseExceptionCode = BaseExceptionCode.Dublicate;
        }
    }
}

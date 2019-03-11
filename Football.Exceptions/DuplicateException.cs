using System;
using System.Collections.Generic;
using System.Text;

namespace Football.Exceptions
{
    public class DuplicateException : BaseException
    {
        public DuplicateException() : base()
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
            this.BaseExceptionCode = BaseExceptionCode.Dublicate;
        }

        public DuplicateException(string message) : base(message)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
            this.BaseExceptionCode = BaseExceptionCode.Dublicate;
        }

        public DuplicateException(string message, Exception innerException) : base(message, innerException)
        {
            this.HttpStatusCode = System.Net.HttpStatusCode.Conflict;
            this.BaseExceptionCode = BaseExceptionCode.Dublicate;
        }
    }
}

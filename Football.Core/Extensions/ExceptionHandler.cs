using Football.Core.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Football.Core.Extensions
{
    public static class ExceptionHandler
    {
        public static Task HandleExceptionAsync(this Exception exception, HttpResponse response, bool isDebug)
        {
            if (exception is AggregateException ae)
            {
                return HandleExceptionAsync(ae.InnerException, response, isDebug);
            }
            if (exception is BaseException be)
            {
                return WriteExceptionAsync(be, response, isDebug);
            }

            return WriteExceptionAsync(exception, response, isDebug);
        }

        /// <summary>
        /// Writes the exception asynchronous.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <param name="response">The response.</param>
        /// <param name="isDebug">if set to <c>true</c> [is debug].</param>
        /// <returns>
        /// Void type
        /// </returns>
        private static Task WriteExceptionAsync(Exception exception, HttpResponse response, bool isDebug)
        {
            response.ContentType = "application/json";

            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var result = new
            {
                Message = isDebug ? $"{exception.Message}. StackTrace: {exception.StackTrace} " : exception.Message
            };

            return response.WriteAsync(JsonConvert.SerializeObject(result));
        }

        private static Task WriteExceptionAsync(BaseException baseException, HttpResponse response, bool isDebug)
        {
            response.ContentType = "application/json";

            response.StatusCode = (int)baseException.HttpStatusCode;

            var result = new
            {
                Message = baseException.Message,
                Code = baseException.BaseExceptionCode
            };

            return response.WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}

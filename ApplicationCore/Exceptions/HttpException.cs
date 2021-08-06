using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions
{
    public class HttpException : Exception
    {
        public HttpException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
        public object Errors { get; set; }
        public HttpStatusCode Code { get; }


    }
}

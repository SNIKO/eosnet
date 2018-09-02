using System;

namespace EOS.Client.Utils
{
    public class HttpResponseException : Exception
    {
        public HttpResponseException(int code, string message)
            : base(message)
        {
            Code = code;
        }

        public int Code { get; }
    }
}
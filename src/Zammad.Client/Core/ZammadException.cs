using System;
using System.Net;
using System.Net.Http;

namespace Zammad.Client.Core
{
    public class ZammadException : Exception
    {
        public ZammadException(HttpRequestMessage request, HttpResponseMessage response)
            : base(GetMessage(response))
        {
            Request = request;
            Response = response;
        }

        private static string GetMessage(HttpResponseMessage response)
        {
            return response.ReasonPhrase;
        }

        public HttpRequestMessage Request { get; }
        public HttpResponseMessage Response { get; }
        public HttpStatusCode? Code => Response?.StatusCode;
    }
}

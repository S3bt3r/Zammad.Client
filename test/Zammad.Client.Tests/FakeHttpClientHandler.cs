using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zammad.Client
{
    public class FakeHttpClientHandler : HttpMessageHandler
    {
        private readonly Action<HttpRequestMessage> _assertRequest;
        private readonly Func<HttpResponseMessage> _returnResult;

        public FakeHttpClientHandler(Action<HttpRequestMessage> assertRequest, Func<HttpResponseMessage> returnResult)
        {
            _assertRequest = assertRequest;
            _returnResult = returnResult;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _assertRequest(request);
            return Task.FromResult(_returnResult());
        }
    }
}
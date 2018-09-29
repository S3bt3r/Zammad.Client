using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Zammad.Client.Core.Protocol
{
    public abstract class HttpClientHandlerBase : HttpClientHandler
    {
        private readonly string _onBehalfOf;

        public HttpClientHandlerBase(string onBehalfOf)
        {
            _onBehalfOf = onBehalfOf;
        }

        protected bool UseBehalfOf => string.IsNullOrEmpty(_onBehalfOf) ? false : true;

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (UseBehalfOf)
            {
                request.Headers.Add("X-On-Behalf-Of", _onBehalfOf);
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}

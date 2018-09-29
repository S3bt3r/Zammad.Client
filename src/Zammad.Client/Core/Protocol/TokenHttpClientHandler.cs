using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Zammad.Client.Core.Protocol
{
    public class TokenHttpClientHandler : HttpClientHandlerBase
    {
        private readonly AuthenticationHeaderValue _authenticationHeader;

        public TokenHttpClientHandler(string token, string onBehalfOf)
            : base(onBehalfOf)
        {
            ArgumentCheck.ThrowIfNullOrEmpty(token, nameof(token));

            _authenticationHeader = CreateAuthenticationHeader(token);
        }

        private AuthenticationHeaderValue CreateAuthenticationHeader(string token)
        {
            return new AuthenticationHeaderValue("Token", $"token={token}");
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = _authenticationHeader;
            return base.SendAsync(request, cancellationToken);
        }
    }
}

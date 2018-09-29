using System;
using System.Net.Http;
using System.Threading.Tasks;
using Zammad.Client.Core.Protocol;

namespace Zammad.Client.Core
{
    public abstract class ZammadClient
    {
        private readonly ZammadAccount _account;

        protected ZammadClient(ZammadAccount account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected HttpRequestBuilder NewRequest()
        {
            return new HttpRequestBuilder();
        }

        protected async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest)
        {
            using (var httpClient = CreateHttpClient())
            {
                var httpResponse = await httpClient.SendAsync(httpRequest);
                if (httpResponse.IsSuccessStatusCode == false)
                {
                    throw new ZammadException(httpRequest, httpResponse);
                }
                return httpResponse;
            }
        }

        private HttpClient CreateHttpClient()
        {
            return new HttpClient(CreateHttpHandler());
        }

        private HttpClientHandler CreateHttpHandler()
        {
            switch (_account.Authentication)
            {
                case ZammadAuthentication.Basic: return new BasicHttpClientHandler(_account.User, _account.Password, _account.OnBehalfOf);
                case ZammadAuthentication.Token: return new TokenHttpClientHandler(_account.Token, _account.OnBehalfOf);
                default: throw new NotImplementedException();
            }
        }

        protected HttpResponseParser NewParser(HttpResponseMessage httpResponse)
        {
            return new HttpResponseParser()
                .UseHttpResponse(httpResponse);
        }

        protected async Task<TResult> GetAsync<TResult>(string path, string query = null)
        {
            var httpRequest = NewRequest()
                .UseGet()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseQuery(query)
                .Build();

            var httpResponse = await SendAsync(httpRequest);

            var result = await NewParser(httpResponse)
                .ParseAsync<TResult>();

            return result;
        }

        protected async Task<TResult> PostAsync<TResult>(string path, object content = null)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendAsync(httpRequest);

            var result = await NewParser(httpResponse)
                .ParseAsync<TResult>();

            return result;
        }

        protected async Task<TResult> PutAsync<TResult>(string path, object content = null)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePut()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendAsync(httpRequest);

            var result = await NewParser(httpResponse)
                .ParseAsync<TResult>();

            return result;
        }

        protected async Task<TResult> DeleteAsync<TResult>(string path, object content = null)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseDelete()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendAsync(httpRequest);

            var result = await NewParser(httpResponse)
                .ParseAsync<TResult>();

            return result;
        }
    }
}

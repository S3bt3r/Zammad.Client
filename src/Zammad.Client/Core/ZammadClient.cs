using System;
using System.IO;
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

        protected Task<TResult> GetAsync<TResult>(string path)
        {
            return GetAsync<TResult>(path, string.Empty);
        }

        protected async Task<TResult> GetAsync<TResult>(string path, string query)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseQuery(query)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return await new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseJsonContentAsync<TResult>();
        }


        protected async Task<bool> GetAsync(string path, string query)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseQuery(query)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();
        }

        protected async Task<Stream> GetAsync(string path)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return await new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseStreamContentAsync();
        }

        protected async Task<bool> PostAsync(string path)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();
        }

        protected async Task<bool> PostAsync(string path, object content)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();
        }

        protected async Task<TResult> PostAsync<TResult>(string path, object content)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return await new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseJsonContentAsync<TResult>();
        }
        
        protected async Task<bool> PutAsync(string path, object content)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePut()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();
        }

        protected async Task<TResult> PutAsync<TResult>(string path, object content)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePut()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return await new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseJsonContentAsync<TResult>();
        }

        protected async Task<bool> DeleteAsync(string path)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseDelete()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();
        }

        protected async Task<bool> DeleteAsync(string path, string content)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseDelete()
                .UseRequestUri(_account.Endpoint)
                .AddPath(path)
                .UseJsonContent(content)
                .Build();

            var httpResponse = await SendHttpRequestAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            return new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();
        }

        protected async Task<HttpResponseMessage> SendHttpRequestAsync(HttpRequestMessage httpRequest)
        {
            using (var httpClient = CreateHttpClient())
            {
                return await httpClient.SendAsync(httpRequest);
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
                case ZammadAuthentication.Basic: return new BasicHttpClientHandler(_account.User, _account.Password);
                case ZammadAuthentication.Token: return new TokenHttpClientHandler(_account.Token);
                default: throw new NotImplementedException();
            }
        }
    }
}

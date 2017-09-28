using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Zammad.Client.Core
{
    public abstract class ZammadClient
    {
        private readonly ZammadAccount _account;

        protected ZammadClient(ZammadAccount account)
        {
            _account = account ?? throw new ArgumentNullException(nameof(account));
        }

        protected Task<TResponse> ExecuteAsync<TResponse>(HttpMethod method, string path)
        {
            return ExecuteAsync<TResponse>(method, path, string.Empty, null);
        }

        protected Task<TResponse> ExecuteAsync<TResponse>(HttpMethod method, string path, string query)
        {
            return ExecuteAsync<TResponse>(method, path, query, null);
        }

        protected Task<TResponse> ExecuteAsync<TResponse>(HttpMethod method, string path, object content)
        {
            return ExecuteAsync<TResponse>(method, path, string.Empty, content);
        }

        protected async Task<TResponse> ExecuteAsync<TResponse>(HttpMethod method, string path, string query, object content)
        {
            if (method == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(method)} must be set.");
            }

            if (path == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(path)} must be set and can not be empty.");
            }

            if (query == null)
            {
                throw new ArgumentNullException($"Parameter {nameof(query)} must be set.");
            }

            using (var httpRequest = BuildHttpRequest(method, path, query, content))
            using (var httpResponse = await SendHttpRequestAsync(httpRequest))
            {
                if (typeof(TResponse) == typeof(bool))
                {
                    return (TResponse)(object)httpResponse.IsSuccessStatusCode;
                }
                else
                {
                    return await ParseHttpResponseAsync<TResponse>(httpResponse);
                }
            }
        }

        private HttpRequestMessage BuildHttpRequest(HttpMethod method, string path, string query, object content)
        {
            var requestUriBuilder = new UriBuilder()
            {
                Scheme = _account.Schema,
                Host = _account.Host,
                Path = path,
                Query = query
            };

            var httpRequest = new HttpRequestMessage(method, requestUriBuilder.Uri);

            if (content != null)
            {
                using (var httpRequestStream = new MemoryStream())
                using (var streamWriter = new StreamWriter(httpRequestStream))
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    var serializer = new JsonSerializer();
                    serializer.Serialize(jsonWriter, content);

                    httpRequest.Content = new StreamContent(httpRequestStream);
                    httpRequest.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                }
            }

            switch (_account.AuthMethod)
            {
                case ZammadAuthMethod.Basic:
                    {
                        var basicAuthValue = $"{_account.User}:{_account.Password}";
                        var basicAuthBytes = Encoding.GetEncoding("ISO-8859-1").GetBytes(basicAuthValue);
                        var basicAuthParameter = Convert.ToBase64String(basicAuthBytes);
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", basicAuthParameter);
                        break;
                    }
                case ZammadAuthMethod.Token:
                    {
                        var tokenAuthParameter = $"token={_account.Token}";
                        httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Token", tokenAuthParameter);
                        break;
                    }
                default:
                    {
                        throw new NotSupportedException($"Authentication method {_account.AuthMethod} is not supported.");
                    }
            }

            return httpRequest;
        }

        private async Task<HttpResponseMessage> SendHttpRequestAsync(HttpRequestMessage requestMessage)
        {
            using (var httpClient = _account.CreateHttpClient())
            {
                return await httpClient.SendAsync(requestMessage);
            }
        }

        private async Task<TResponse> ParseHttpResponseAsync<TResponse>(HttpResponseMessage httpResponse)
        {
            httpResponse.EnsureSuccessStatusCode();

            if (httpResponse.Content.Headers.ContentLength.HasValue &&
                httpResponse.Content.Headers.ContentLength == 0)
            {
                return default(TResponse);
            }

            if (string.Compare(httpResponse.Content.Headers.ContentType.MediaType, "application/json", true) != 0)
            {
                throw new NotSupportedException($"Content media type {httpResponse.Content.Headers.ContentType.MediaType} is not supported.");
            }

            if (string.Compare(httpResponse.Content.Headers.ContentType.CharSet, "utf-8", true) != 0)
            {
                throw new NotSupportedException($"Content charset {httpResponse.Content.Headers.ContentType.CharSet} is not supported.");
            }

            using (var httpResponseStream = await httpResponse.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(httpResponseStream, Encoding.UTF8))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                var serializer = new JsonSerializer();
                return serializer.Deserialize<TResponse>(jsonReader);
            }
        }
    }
}

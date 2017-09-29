using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Zammad.Client.Core.Protocol
{
    public class TokenHttpClientHandlerTest
    {
        [Theory]
        [InlineData("token1", "token=token1")]
        [InlineData("token2", "token=token2")]
        public async Task TokenHttpClientHandler_Success_Test(string token, string expected)
        {
            using (var httpHandler = new TokenHttpClientHandler(token))
            using (var httpClient = new HttpClient(httpHandler))
            {
                var response = await httpClient.GetAsync("http://zammad.com");
                Assert.Equal("Token", response.RequestMessage.Headers.Authorization.Scheme);
                Assert.Equal(expected, response.RequestMessage.Headers.Authorization.Parameter);
            }
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void TokenHttpClientHandler_Fail_Test(string token)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var httpHandler = new TokenHttpClientHandler(token);
            });
        }
    }
}

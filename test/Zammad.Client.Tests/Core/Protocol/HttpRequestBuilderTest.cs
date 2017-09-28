using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Zammad.Client.Core.Protocol
{
    public class HttpRequestBuilderTest
    {
        [Fact]
        public void UseGet_Success_Test()
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .Build();

            Assert.Equal(HttpMethod.Get, httpRequest.Method);
        }

        [Fact]
        public void UsePost_Success_Test()
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .Build();

            Assert.Equal(HttpMethod.Post, httpRequest.Method);
        }

        [Fact]
        public void UsePut_Success_Test()
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePut()
                .Build();

            Assert.Equal(HttpMethod.Put, httpRequest.Method);
        }

        [Fact]
        public void UseDelete_Success_Test()
        {
            var httpRequest = new HttpRequestBuilder()
                .UseDelete()
                .Build();

            Assert.Equal(HttpMethod.Delete, httpRequest.Method);
        }

        [Theory]
        [InlineData("http://test.zammad.com", "http://test.zammad.com/")]
        [InlineData("https://test.zammad.com", "https://test.zammad.com/")]
        [InlineData("http://localhost/zammad", "http://localhost/zammad")]
        [InlineData("http://localhost:8080/zammad", "http://localhost:8080/zammad")]
        public void UseRequestUri_Success_Test(string requestUri, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .UseRequestUri(requestUri)
                .Build();

            Assert.Equal(expected, httpRequest.RequestUri.AbsoluteUri);
        }

        [Theory]
        [InlineData("://zammad.com")]
        [InlineData("zammad.com")]
        [InlineData("http://test..com")]
        [InlineData("http://.com")]
        public void UseRequestUri_Fail_Test(string requestUri)
        {
            Assert.ThrowsAny<UriFormatException>(() =>
            {
                var httpRequest = new HttpRequestBuilder()
                    .UseGet()
                    .UseRequestUri(requestUri)
                    .Build();
            });
        }

        [Theory]
        [InlineData("/api/v1/tickets", "/api/v1/tickets")]
        [InlineData("/api/v1/groups", "/api/v1/groups")]
        public void AddPath_Success_Test(string path, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .AddPath(path)
                .Build();

            Assert.Equal(expected, httpRequest.RequestUri.AbsolutePath);
        }

        [Theory]
        [InlineData("/api/v1/tickets", "123", "/api/v1/tickets/123")]
        [InlineData("/api/v1/groups", "123", "/api/v1/groups/123")]
        public void AddPath_2_Success_Test(string path1, string path2, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .AddPath(path1)
                .AddPath(path2)
                .Build();

            Assert.Equal(expected, httpRequest.RequestUri.AbsolutePath);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void AddPath_Fail_Test(string path)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var httpRequest = new HttpRequestBuilder()
                .UseGet()
                    .AddPath(path)
                    .Build();
            });
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("query=zammad", "?query=zammad")]
        [InlineData("?query=zammad", "?query=zammad")]
        [InlineData("?query=", "?query=")]
        [InlineData("?query=zammad&limit=10", "?query=zammad&limit=10")]
        public void UseQuery_Success_Test(string query, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .UseQuery(query)
                .Build();

            Assert.Equal(expected, httpRequest.RequestUri.Query);
        }

        [Theory]
        [InlineData(null)]
        public void UseQuery_Fail_Test(string query)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var httpRequest = new HttpRequestBuilder()
                .UseGet()
                    .UseQuery(query)
                    .Build();
            });
        }

        [Theory]
        [InlineData("query", "zammad", "?query=zammad")]
        [InlineData("query", "", "?query=")]
        public void AddQuery_Success_Test(string key, string value, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .AddQuery(key, value)
                .Build();

            Assert.Equal(expected, httpRequest.RequestUri.Query);
        }

        [Theory]
        [InlineData("query", "zammad", "limit", "1", "?query=zammad&limit=1")]
        [InlineData("query", "zammad", "limit", "", "?query=zammad&limit=")]
        public void AddQuery_2_Success_Test(string key1, string value1, string key2, string value2, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .AddQuery(key1, value1)
                .AddQuery(key2, value2)
                .Build();

            Assert.Equal(expected, httpRequest.RequestUri.Query);
        }

        [Theory]
        [InlineData("", "\"\"")]
        [InlineData("zammad", "\"zammad\"")]
        public async Task UseJsonContent_Success_Test(string content, string expected)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .UseJsonContent(content)
                .Build();

            var testContent = await httpRequest.Content.ReadAsStringAsync();
            Assert.Equal(expected, testContent);
        }

        [Theory]
        [InlineData(null)]
        public void UseJsonContent_Fail_Test(string content)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var httpRequest = new HttpRequestBuilder()
                    .UsePost()
                    .UseJsonContent(content)
                    .Build();
            });
        }

        [Theory]
        [InlineData("http://test.zammad.com", "/api/tickets", "expand=true", "zammad", "http://test.zammad.com/api/tickets?expand=true", "\"zammad\"")]
        [InlineData("http://test.zammad.com", "/api/tickets", "", "zammad", "http://test.zammad.com/api/tickets", "\"zammad\"")]
        [InlineData("http://test.zammad.com", "/api/tickets", "expand=true", "", "http://test.zammad.com/api/tickets?expand=true", "\"\"")]
        public async Task Build_Post_Success_Test(string requestUri, string path, string query, string content, string expectedUri, string expectedContent)
        {
            var httpRequest = new HttpRequestBuilder()
                .UsePost()
                .UseRequestUri(requestUri)
                .AddPath(path)
                .UseQuery(query)
                .UseJsonContent(content)
                .Build();

            Assert.Equal(expectedUri, httpRequest.RequestUri.AbsoluteUri);

            var testContent = await httpRequest.Content.ReadAsStringAsync();
            Assert.Equal(expectedContent, testContent);
        }

        [Theory]
        [InlineData("http://test.zammad.com", "/api/tickets", "expand=true", "http://test.zammad.com/api/tickets?expand=true")]
        [InlineData("http://test.zammad.com", "/api/tickets", "", "http://test.zammad.com/api/tickets")]
        public void Build_Get_Success_Test(string requestUri, string path, string query, string expectedUri)
        {
            var httpRequest = new HttpRequestBuilder()
                .UseGet()
                .UseRequestUri(requestUri)
                .AddPath(path)
                .UseQuery(query)
                .Build();

            Assert.Equal(expectedUri, httpRequest.RequestUri.AbsoluteUri);
        }

        [Fact]
        public void Build_Fail_Test()
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var httpRequest = new HttpRequestBuilder()
                    .Build();
            });
        }
    }
}
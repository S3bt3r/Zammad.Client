using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Zammad.Client.Core.Protocol
{
    public class HttpResponseParserTest
    {
        [Fact]
        public void UseHttpResponse_Success_Test()
        {
            var httpResponse = CreateTestResponse();

            var httpResponseParser = new HttpResponseParser()
                .UseHttpResponse(httpResponse);
        }

        [Fact]
        public void UseHttpResponse_Fail_Test()
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var httpResponseParser = new HttpResponseParser()
                .UseHttpResponse(null);
            });
        }

        [Fact]
        public void ParseSuccessStatus_Success_Test()
        {
            var httpResponse = CreateTestResponse();

            var success = new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseSuccessStatus();

            Assert.Equal(true, success);
        }

        [Fact]
        public void ParseStatusCode_Success_Test()
        {
            var httpResponse = CreateTestResponse();

            var httpStatusCode = new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseStatusCode();

            Assert.Equal(HttpStatusCode.OK, httpStatusCode);
        }

        [Fact]
        public void ParseStatusCodeValue_Success_Test()
        {
            var httpResponse = CreateTestResponse();

            var statusCode = new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseStatusCodeValue();

            Assert.Equal(200, statusCode);
        }

        [Fact]
        public async Task ParseJsonContentAsync_Success_TestAsync()
        {
            var httpResponse = CreateTestResponse();

            var ticket = await new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseJsonContentAsync<Ticket.Ticket>();

            Assert.Equal(1, ticket.Id);
            Assert.Equal(1, ticket.GroupId);
            Assert.Equal(2, ticket.PriorityId);
            Assert.Equal(1, ticket.StateId);
            Assert.Equal(1, ticket.OrganizationId);
            Assert.Equal("96001", ticket.Number);
            Assert.Equal("Welcome to Zammad!", ticket.Title);
            Assert.Equal(1, ticket.OwnerId);
            Assert.Equal(2, ticket.CustomerId);
            Assert.Equal(null, ticket.Note);
            Assert.Equal(null, ticket.FirstResponseAt);
            Assert.Equal(null, ticket.FirstResponseEscalationAt);
            Assert.Equal(null, ticket.FirstResponseInMin);
            Assert.Equal(null, ticket.FirstResponseDiffInMin);
            Assert.Equal(null, ticket.CloseAt);
            Assert.Equal(null, ticket.CloseEscalationAt);
            Assert.Equal(null, ticket.CloseInMin);
            Assert.Equal(null, ticket.CloseDiffInMin);
            Assert.Equal(null, ticket.UpdateEscalationAt);
            Assert.Equal(null, ticket.UpdateInMin);
            Assert.Equal(null, ticket.UpdateDiffInMin);
            Assert.Equal(DateTimeOffset.Parse("2017-09-25T14:50:50.946Z"), ticket.LastContactAt);
            Assert.Equal(null, ticket.LastContactAgentAt);
            Assert.Equal(DateTimeOffset.Parse("2017-09-25T14:50:50.946Z"), ticket.LastContactCustomerAt);
            Assert.Equal(null, ticket.LastOwnerUpdateAt);
            Assert.Equal(5, ticket.CreateArticleTypeId);
            Assert.Equal(2, ticket.CreateArticleSenderId);
            Assert.Equal(1, ticket.ArticleCount);
            Assert.Equal(null, ticket.EscalationAt);
            Assert.Equal(null, ticket.PendingTime);
            Assert.Equal(null, ticket.Type);
            Assert.Equal(null, ticket.TimeUnit);
            Assert.Equal(new Dictionary<string, object>(), ticket.Preferences);
            Assert.Equal(3, ticket.UpdatedById);
            Assert.Equal(2, ticket.CreatedById);
            Assert.Equal(DateTimeOffset.Parse("2017-09-25T14:50:50.910Z"), ticket.CreatedAt);
            Assert.Equal(DateTimeOffset.Parse("2017-09-30T14:47:55.177Z"), ticket.UpdatedAt);
        }

        [Fact]
        public async Task ParseStreamContentAsync_Success_TestAsync()
        {
            var httpResponse = CreateTestResponse();

            using (var ticketStream = await new HttpResponseParser()
                .UseHttpResponse(httpResponse)
                .ParseStreamContentAsync())
            using (var reader = new StreamReader(ticketStream))
            {
                var ticketString = await reader.ReadToEndAsync();
                Assert.Equal(TestConstants.TicketSerialized, ticketString);
            }
        }

        private HttpResponseMessage CreateTestResponse()
        {
            return new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(TestConstants.TicketSerialized, Encoding.UTF8, "application/json")
            };
        }
    }
}

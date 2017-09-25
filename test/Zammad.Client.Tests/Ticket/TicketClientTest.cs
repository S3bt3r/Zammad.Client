using System;
using Xunit;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using Zammad.Client.Core;
using System.Text;

namespace Zammad.Client.Ticket
{
    public class TicketClientTest
    {
        [Fact]
        public void Constructor_Success_Test()
        {
            var account = CreateTestAccount();
            var client = new TicketClient(account);
        }

        [Fact]
        public void Constructor_Throws_Test()
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var client = new TicketClient(null);
            });
        }

        [Fact]
        public async Task GetTicketListAsync_Test()
        {
            var account = CreateTestAccount(AssertGetTicketListAsync, ResultGetTicketListAsync);
            var client = new TicketClient(account);
            var list = await client.GetTicketListAsync();
            Assert.NotEmpty(list);
        }

        private void AssertGetTicketListAsync(HttpRequestMessage requestMessage)
        {
            Assert.Equal(HttpMethod.Get, requestMessage.Method);
            Assert.Equal(new Uri("https://test.zammad.com/api/v1/tickets"), requestMessage.RequestUri);
        }

        private HttpResponseMessage ResultGetTicketListAsync()
        {
            return new HttpResponseMessage()
            {
                Content = new StringContent("[{\"id\":1,\"group_id\":1,\"priority_id\":2,\"state_id\":1,\"organization_id\":1,\"number\":\"96001\",\"title\":\"Welcome to Zammad!\",\"owner_id\":1,\"customer_id\":2,\"note\":null,\"first_response_at\":null,\"first_response_escalation_at\":null,\"first_response_in_min\":null,\"first_response_diff_in_min\":null,\"close_at\":null,\"close_escalation_at\":null,\"close_in_min\":null,\"close_diff_in_min\":null,\"update_escalation_at\":null,\"update_in_min\":null,\"update_diff_in_min\":null,\"last_contact_at\":\"2017-09-25T14:50:50.946Z\",\"last_contact_agent_at\":null,\"last_contact_customer_at\":\"2017-09-25T14:50:50.946Z\",\"last_owner_update_at\":null,\"create_article_type_id\":5,\"create_article_sender_id\":2,\"article_count\":1,\"escalation_at\":null,\"pending_time\":null,\"type\":null,\"time_unit\":null,\"preferences\":{},\"updated_by_id\":2,\"created_by_id\":2,\"created_at\":\"2017-09-25T14:50:50.910Z\",\"updated_at\":\"2017-09-25T14:50:50.964Z\"}]", Encoding.UTF8, "application/json")
            };
        }

        public ZammadAccount CreateTestAccount()
        {
            var mock = new Mock<ZammadAccount>(TestConstants.AccountSchema, TestConstants.AccountHost, ZammadAuthMethod.Basic, TestConstants.AccountUser, TestConstants.AccountPassword, null);
            return mock.Object;
        }

        public ZammadAccount CreateTestAccount(Action<HttpRequestMessage> assertRequest, Func<HttpResponseMessage> returnResult)
        {
            var mock = new Mock<ZammadAccount>(TestConstants.AccountSchema, TestConstants.AccountHost, ZammadAuthMethod.Basic, TestConstants.AccountUser, TestConstants.AccountPassword, null);
            mock.Setup(x => x.CreateHttpClient()).Returns(new HttpClient(new FakeHttpClientHandler(assertRequest, returnResult)));
            return mock.Object;
        }
    }
}

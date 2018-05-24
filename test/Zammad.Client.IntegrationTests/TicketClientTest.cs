using System.Threading.Tasks;
using Xunit;

namespace Zammad.Client.IntegrationTests
{
    [TestCaseOrderer("Zammad.Client.IntegrationTests.TestOrderer", "Zammad.Client.IntegrationTests")]
    public class TicketClientTest
    {
        [Fact, Order(TestOrder.TicketSearch)]
        public async Task Ticket_Search_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateTicketClient();
            
            var ticketSearch = await client.SearchTicketAsync("Zammad", 20);
            Assert.NotNull(ticketSearch);
        }
    }
}

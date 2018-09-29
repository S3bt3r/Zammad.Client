using System.Threading.Tasks;
using Xunit;
using Zammad.Client.Resources;

namespace Zammad.Client.IntegrationTests
{
    [TestCaseOrderer("Zammad.Client.IntegrationTests.TestOrderer", "Zammad.Client.IntegrationTests")]
    public class TicketClientTest
    {
        [Fact, Order(TestOrder.TicketCreate)]
        public async Task Ticket_Create_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateTicketClient();

            var ticket = await client.CreateTicketAsync(
                new Ticket
                {
                    Title = "Help me!",
                    GroupId = 1,
                    CustomerId = 1,
                    OwnerId = 1
                },
                new TicketArticle
                {
                    Subject = "Help me!!!",
                    Body = "Nothing Work!",
                    Type = "note",
                });

            Assert.NotNull(ticket);
        }

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

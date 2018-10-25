using System.Threading.Tasks;
using Xunit;

namespace Zammad.Client.IntegrationTests
{
    [TestCaseOrderer("Zammad.Client.IntegrationTests.TestOrderer", "Zammad.Client.IntegrationTests")]
    public class TagClientTest
    {
        [Fact, Order(TestOrder.TagGetTagList)]
        public async Task Tag_TagGetTagList_Test()
        {
            var account = TestHelper.CreateTestAccount();
            var client = account.CreateTagClient();

            var tagList = await client.GetTagListAsync("Ticket", 1);
            Assert.NotNull(tagList);
        }
    }
}

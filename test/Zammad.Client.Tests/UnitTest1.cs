using System;
using System.Threading.Tasks;
using Xunit;

namespace Zammad.Client.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1Async()
        {
            var account = ZammadAccount.CreateTokenAccount("http", "zammad-soft", "vatTlijkbmZYcIYQ1qhw8t6FIENwclwXYKQKPah34GKlolMb5e7jzZdxYjHY-IMP");

            var groupClient = account.CreateGroupClient();
            var groupList = await groupClient.GetGroupListAsync();

            var notificationClient = account.CreateOnlineNotificationClient();
            var notificationList = await notificationClient.GetOnlineNotificationListAsync();

            var organizationClient = account.CreateOrganizationClient();
            var organizationList = await organizationClient.GetOrganizationListAsync();

            var ticketClient = account.CreateTicketClient();
            var ticketList = await ticketClient.GetTicketListAsync();
            var ticketArticleList = await ticketClient.GetTicketArticleListAsync();
            var ticketPriorityList = await ticketClient.GetTicketPriorityListAsync();
            var ticketStateList = await ticketClient.GetTicketStateListAsync();

            var userClient = account.CreateUserClient();
            var userList = await userClient.GetUserListAsync();
        }
    }
}

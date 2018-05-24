
namespace Zammad.Client.IntegrationTests
{
    public static class TestHelper
    {
        public static ZammadAccount CreateTestAccount()
        {
            return ZammadAccount.CreateBasicAccount(TestConstants.AccountEndpoint, TestConstants.AccountUser, TestConstants.AccountPassword);
        }
    }
}

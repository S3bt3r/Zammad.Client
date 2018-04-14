using System;
using Xunit;

namespace Zammad.Client
{
    public class ZammadAccountTest
    {
        [Theory]
        [InlineData("http://test.zammad.com/", "user", "password")]
        [InlineData("https://test.zammad.com/", "user", "password")]
        public void CreateBasicAccount_Success_Test(string endpoint, string user, string password)
        {
            var account = ZammadAccount.CreateBasicAccount(endpoint, user, password);
            Assert.Equal(endpoint, account.Endpoint.AbsoluteUri);
            Assert.Equal(user, account.User);
            Assert.Equal(password, account.Password);
            Assert.Null(account.Token);
        }

        [Theory]
        [InlineData("http://", "user", "password")]
        public void CreateBasicAccount_Uri_Fail_Test(string endpoint, string user, string password)
        {
            Assert.ThrowsAny<UriFormatException>(() =>
            {
                var account = ZammadAccount.CreateBasicAccount(endpoint, user, password);
            });
        }

        [Theory]
        [InlineData("http://test.zammad.com/", "", "password")]
        [InlineData("http://test.zammad.com/", "user", "")]
        public void CreateBasicAccount_UserPassword_Fail_Test(string endpoint, string user, string password)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var account = ZammadAccount.CreateBasicAccount(endpoint, user, password);
            });
        }

        [Theory]
        [InlineData("http://test.zammad.com/", "token")]
        [InlineData("https://test.zammad.com/", "token")]
        public void CreateTokenAccount_Success_Test(string endpoint, string token)
        {
            var account = ZammadAccount.CreateTokenAccount(endpoint, token);
            Assert.Equal(endpoint, account.Endpoint.AbsoluteUri);
            Assert.Null(account.User);
            Assert.Null(account.Password);
            Assert.Equal(token, account.Token);
        }

        [Theory]
        [InlineData("http://", "token")]
        public void CreateTokenAccount_Uri_Fail_Test(string endpoint, string token)
        {
            Assert.ThrowsAny<UriFormatException>(() =>
            {
                var account = ZammadAccount.CreateTokenAccount(endpoint, token);
            });
        }

        [Theory]
        [InlineData("http://test.zammad.com/", "")]
        public void CreateTokenAccount_Token_Fail_Test(string endpoint, string token)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var account = ZammadAccount.CreateTokenAccount(endpoint, token);
            });
        }

        [Fact]
        public void CreateGroupClient_Success_Test()
        {
            var client = CreateTestAccount().CreateGroupClient();
            Assert.NotNull(client);
        }

        [Fact]
        public void CreateOnlineNotificationClient_Success_Test()
        {
            var client = CreateTestAccount().CreateOnlineNotificationClient();
            Assert.NotNull(client);
        }

        [Fact]
        public void CreateOrganizationClient_Success_Test()
        {
            var client = CreateTestAccount().CreateOrganizationClient();
            Assert.NotNull(client);
        }

        [Fact]
        public void CreateTicketClient_Success_Test()
        {
            var client = CreateTestAccount().CreateTicketClient();
            Assert.NotNull(client);
        }

        [Fact]
        public void CreateUserClient_Success_Test()
        {
            var client = CreateTestAccount().CreateUserClient();
            Assert.NotNull(client);
        }

        private ZammadAccount CreateTestAccount()
        {
            return ZammadAccount.CreateBasicAccount(TestConstants.AccountEndpoint, TestConstants.AccountUser, TestConstants.AccountPassword);
        }
    }
}

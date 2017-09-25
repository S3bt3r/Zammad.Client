using System;
using Xunit;

namespace Zammad.Client
{
    public class ZammadAccountTest
    {
        [Theory]
        [InlineData("http", "test.zammad.com", "user", "password")]
        [InlineData("https", "test.zammad.com", "user", "password")]
        public void CreateBasicAccount_Success_Test(string schema, string host, string user, string password)
        {
            var account = ZammadAccount.CreateBasicAccount(schema, host, user, password);
            Assert.Equal(schema, account.Schema);
            Assert.Equal(host, account.Host);
            Assert.Equal(user, account.User);
            Assert.Equal(password, account.Password);
            Assert.Equal(null, account.Token);
        }

        [Theory]
        [InlineData("tcp", "test.zammad.com", "user", "password")]
        [InlineData("http", "", "user", "password")]
        [InlineData("http", "test.zammad.com", "", "password")]
        [InlineData("http", "test.zammad.com", "user", "")]
        public void CreateBasicAccount_Throws_Test(string schema, string host, string user, string password)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var account = ZammadAccount.CreateBasicAccount(schema, host, user, password);
            });
        }

        [Theory]
        [InlineData("http", "test.zammad.com", "token")]
        [InlineData("https", "test.zammad.com", "token")]
        public void CreateTokenAccount_Success_Test(string schema, string host, string token)
        {
            var account = ZammadAccount.CreateTokenAccount(schema, host, token);
            Assert.Equal(schema, account.Schema);
            Assert.Equal(host, account.Host);
            Assert.Equal(null, account.User);
            Assert.Equal(null, account.Password);
            Assert.Equal(token, account.Token);
        }

        [Theory]
        [InlineData("tcp", "test.zammad.com", "token")]
        [InlineData("http", "", "token")]
        [InlineData("http", "test.zammad.com", "")]
        public void CreateTokenAccount_Throws_Test(string schema, string host, string token)
        {
            Assert.ThrowsAny<ArgumentException>(() =>
            {
                var account = ZammadAccount.CreateTokenAccount(schema, host, token);
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

        public ZammadAccount CreateTestAccount()
        {
            return ZammadAccount.CreateBasicAccount(TestConstants.AccountSchema, TestConstants.AccountHost, TestConstants.AccountUser, TestConstants.AccountPassword);
        }
    }
}

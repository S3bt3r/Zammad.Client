using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zammad.Client.Core;

namespace Zammad.Client.User
{
    public class UserClient : ZammadClient, IUserService
    {
        public UserClient(ZammadAccount account)
            : base(account)
        {

        }

        #region IUserService

        public Task<User> GetUserMeAsync()
        {
            return ExecuteAsync<User>(HttpMethod.Get, "/api/v1/users/me");
        }

        public Task<IList<User>> GetUserListAsync()
        {
            return ExecuteAsync<IList<User>>(HttpMethod.Get, "/api/v1/users");
        }

        public Task<IList<User>> GetUserListAsync(int page, int count)
        {
            return ExecuteAsync<IList<User>>(HttpMethod.Get, "/api/v1/users?page={page},per_page={count}");
        }

        public Task<IList<User>> SearchUserAsync(string query, int limit)
        {
            return ExecuteAsync<IList<User>>(HttpMethod.Get, "/api/v1/users", $"?query={query}&limit={limit}");
        }

        public Task<User> GetUserAsync(int id)
        {
            return ExecuteAsync<User>(HttpMethod.Get, "/api/v1/users/{id}");
        }

        public Task<User> CreateUserAsync(User user)
        {
            return ExecuteAsync<User>(HttpMethod.Post, "/api/v1/users", user);
        }

        public Task<User> UpdateUserAsync(int id, User user)
        {
            return ExecuteAsync<User>(HttpMethod.Put, "/api/v1/users/{id}", user);
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/users/{id}");
        }

        #endregion
    }
}

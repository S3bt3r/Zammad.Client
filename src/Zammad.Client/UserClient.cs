using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Core;
using Zammad.Client.Resources;
using Zammad.Client.Services;

namespace Zammad.Client
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
            return GetAsync<User>("/api/v1/users/me");
        }

        public Task<IList<User>> GetUserListAsync()
        {
            return GetAsync<IList<User>>("/api/v1/users");
        }

        public Task<IList<User>> GetUserListAsync(int page, int count)
        {
            return GetAsync<IList<User>>("/api/v1/users", $"?page={page},per_page={count}");
        }

        public Task<IList<User>> SearchUserAsync(string query, int limit)
        {
            return GetAsync<IList<User>>("/api/v1/users/search", $"?query={query}&limit={limit}&expand=true");
        }

        public Task<User> GetUserAsync(int id)
        {
            return GetAsync<User>($"/api/v1/users/{id}");
        }

        public Task<User> CreateUserAsync(User user)
        {
            return PostAsync<User>("/api/v1/users", user);
        }

        public Task<User> UpdateUserAsync(int id, User user)
        {
            return PutAsync<User>($"/api/v1/users/{id}", user);
        }

        public Task<bool> DeleteUserAsync(int id)
        {
            return DeleteAsync($"/api/v1/users/{id}");
        }

        #endregion
    }
}

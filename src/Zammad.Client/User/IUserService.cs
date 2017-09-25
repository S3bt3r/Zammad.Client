using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.User
{
    public interface IUserService
    {
        Task<User> GetUserMeAsync();
        Task<IList<User>> GetUserListAsync();
        Task<IList<User>> GetUserListAsync(int page, int count);
        Task<IList<User>> SearchUserAsync(string query, int limit);
        Task<User> GetUserAsync(int id);
        Task<User> CreateUserAsync(User user);
        Task<User> UpdateUserAsync(int id, User user);
        Task<bool> DeleteUserAsync(int id);
    }
}

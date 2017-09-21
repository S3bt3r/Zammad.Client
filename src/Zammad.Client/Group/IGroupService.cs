using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Group
{
    public interface IGroupService
    {
        Task<IList<Group>> GetGroupListAsync();
        Task<Group> GetGroupAsync(int id);
        Task<Group> CreateGroupAsync(Group group);
        Task<Group> UpdateGroupAsync(int id, Group group);
        Task<bool> DeleteGroupAsync(int id);
    }
}

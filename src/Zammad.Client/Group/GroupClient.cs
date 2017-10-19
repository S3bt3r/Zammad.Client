using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Core;

namespace Zammad.Client.Group
{
    public class GroupClient : ZammadClient, IGroupService
    {
        public GroupClient(ZammadAccount account)
            : base(account)
        {

        }

        #region IGroupService      

        public Task<IList<Group>> GetGroupListAsync()
        {
            return GetAsync<IList<Group>>("/api/v1/groups");
        }

        public Task<IList<Group>> GetGroupListAsync(int page, int count)
        {
            return GetAsync<IList<Group>>("/api/v1/groups", $"?page={page},per_page={count}");
        }

        public Task<Group> GetGroupAsync(int id)
        {
            return GetAsync<Group>($"/api/v1/groups/{id}");
        }

        public Task<Group> CreateGroupAsync(Group group)
        {
            return PostAsync<Group>("/api/v1/groups", group);
        }

        public Task<Group> UpdateGroupAsync(int id, Group group)
        {
            return PutAsync<Group>($"/api/v1/groups/{id}", group);
        }

        public Task<bool> DeleteGroupAsync(int id)
        {
            return DeleteAsync($"/api/v1/groups/{id}");
        }

        #endregion
    }
}

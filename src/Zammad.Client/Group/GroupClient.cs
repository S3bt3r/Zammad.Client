using System.Collections.Generic;
using System.Net.Http;
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
            return ExecuteAsync<IList<Group>>(HttpMethod.Get, "/api/v1/groups");
        }

        public Task<IList<Group>> GetGroupListAsync(int page, int count)
        {
            return ExecuteAsync<IList<Group>>(HttpMethod.Get, "/api/v1/groups?page={page},per_page={count}");
        }

        public Task<Group> GetGroupAsync(int id)
        {
            return ExecuteAsync<Group>(HttpMethod.Get, "/api/v1/groups/{id}");
        }

        public Task<Group> CreateGroupAsync(Group group)
        {
            return ExecuteAsync<Group>(HttpMethod.Post, "/api/v1/groups", group);
        }

        public Task<Group> UpdateGroupAsync(int id, Group group)
        {
            return ExecuteAsync<Group>(HttpMethod.Put, "/api/v1/groups/{id}", group);
        }

        public Task<bool> DeleteGroupAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/groups/{id}");
        }

        #endregion
    }
}

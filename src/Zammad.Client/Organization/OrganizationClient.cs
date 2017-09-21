using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zammad.Client.Core;

namespace Zammad.Client.Organization
{
    public class OrganizationClient : ZammadClient, IOrganizationService
    {
        public OrganizationClient(ZammadAccount account)
            : base(account)
        {

        }

        #region IOrganizationService

        public Task<IList<Organization>> GetOrganizationListAsync()
        {
            return ExecuteAsync<IList<Organization>>(HttpMethod.Get, "/api/v1/organizations");
        }

        public Task<IList<Organization>> SearchOrganizationAsync(string query, int limit)
        {
            return ExecuteAsync<IList<Organization>>(HttpMethod.Get, "/api/v1/organizations", $"?query={query}&limit={limit}");
        }

        public Task<Organization> GetOrganizationAsync(int id)
        {
            return ExecuteAsync<Organization>(HttpMethod.Get, "/api/v1/organizations/{id}");
        }

        public Task<Organization> CreateOrganizationAsync(Organization organization)
        {
            return ExecuteAsync<Organization>(HttpMethod.Post, "/api/v1/organizations", organization);
        }

        public Task<Organization> UpdateOrganizationAsync(int id, Organization organization)
        {
            return ExecuteAsync<Organization>(HttpMethod.Put, "/api/v1/organizations/{id}", organization);
        }

        public Task<bool> DeleteOrganizationAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/organizations/{id}");
        }

        #endregion
    }
}

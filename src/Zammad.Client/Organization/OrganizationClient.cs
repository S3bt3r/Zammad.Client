using System.Collections.Generic;
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
            return GetAsync<IList<Organization>>("/api/v1/organizations");
        }

        public Task<IList<Organization>> GetOrganizationListAsync(int page, int count)
        {
            return GetAsync<IList<Organization>>("/api/v1/organizations?page={page},per_page={count}");
        }

        public Task<IList<Organization>> SearchOrganizationAsync(string query, int limit)
        {
            return GetAsync<IList<Organization>>("/api/v1/organizations", $"?query={query}&limit={limit}");
        }

        public Task<Organization> GetOrganizationAsync(int id)
        {
            return GetAsync<Organization>("/api/v1/organizations/{id}");
        }

        public Task<Organization> CreateOrganizationAsync(Organization organization)
        {
            return PostAsync<Organization>("/api/v1/organizations", organization);
        }

        public Task<Organization> UpdateOrganizationAsync(int id, Organization organization)
        {
            return PutAsync<Organization>("/api/v1/organizations/{id}", organization);
        }

        public Task<bool> DeleteOrganizationAsync(int id)
        {
            return DeleteAsync("/api/v1/organizations/{id}");
        }

        #endregion
    }
}

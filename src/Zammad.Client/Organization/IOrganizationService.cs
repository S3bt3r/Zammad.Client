using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Organization
{
    public interface IOrganizationService
    {
        Task<IList<Organization>> GetOrganizationListAsync();
        Task<IList<Organization>> GetOrganizationListAsync(int page, int count);
        Task<IList<Organization>> SearchOrganizationAsync(string query, int limit);
        Task<Organization> GetOrganizationAsync(int id);
        Task<Organization> CreateOrganizationAsync(Organization organization);
        Task<Organization> UpdateOrganizationAsync(int id, Organization organization);
        Task<bool> DeleteOrganizationAsync(int id);
    }
}

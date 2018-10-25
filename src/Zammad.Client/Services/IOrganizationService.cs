using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Resources;

namespace Zammad.Client.Services
{
    public interface IOrganizationService
    {
        Task<IList<Organization>> GetOrganizationListAsync();
        Task<IList<Organization>> GetOrganizationListAsync(int page, int count);
        Task<IList<Organization>> SearchOrganizationAsync(string query, int limit);
        Task<IList<Organization>> SearchOrganizationAsync(string query, int limit, string sortBy, string orderBy);
        Task<Organization> GetOrganizationAsync(int id);
        Task<Organization> CreateOrganizationAsync(Organization organization);
        Task<Organization> UpdateOrganizationAsync(int id, Organization organization);
        Task<bool> DeleteOrganizationAsync(int id);
    }
}

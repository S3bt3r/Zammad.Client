using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Resources;

namespace Zammad.Client.Services
{
    public interface IObjectService
    {
        Task<IList<Object>> GetObjectListAsync();
        Task<Object> GetObjectAsync(int id);
        Task<Object> CreateObjectAsyc(Object @object);
        Task<Object> UpdateObjectAsync(int id, Object @object);
        Task<bool> ExecuteMigrationAsync();
    }
}

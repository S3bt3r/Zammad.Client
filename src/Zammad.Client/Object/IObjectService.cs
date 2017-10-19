using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.Object
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

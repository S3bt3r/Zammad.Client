using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Core;
using Zammad.Client.Resources;
using Zammad.Client.Services;

namespace Zammad.Client
{
    public class ObjectClient : ZammadClient, IObjectService
    {
        public ObjectClient(ZammadAccount account)
            : base(account)
        {

        }

        #region IObjectService
        
        public Task<IList<Object>> GetObjectListAsync()
        {
            return GetAsync<IList<Object>>("/api/v1/object_manager_attributes");
        }

        public Task<Object> GetObjectAsync(int id)
        {
            return GetAsync<Object>($"/api/v1/object_manager_attributes/{id}");
        }

        public Task<Object> CreateObjectAsyc(Object @object)
        {
            return PostAsync<Object>("/api/v1/object_manager_attributes", @object);
        }

        public Task<Object> UpdateObjectAsync(int id, Object @object)
        {
            return PutAsync<Object>($"/api/v1/object_manager_attributes/{id}", @object);
        }

        public Task<bool> ExecuteMigrationAsync()
        {
            return PostAsync("/api/v1/object_manager_attributes_execute_migrations");
        }

        #endregion
    }
}

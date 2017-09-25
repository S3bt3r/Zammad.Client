using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Zammad.Client.Core;

namespace Zammad.Client.OnlineNotification
{
    public class OnlineNotificationClient : ZammadClient, IOnlineNotificationService
    {
        public OnlineNotificationClient(ZammadAccount account)
            : base(account)
        {

        }

        #region IOnlineNotificationService      

        public Task<IList<OnlineNotification>> GetOnlineNotificationListAsync()
        {
            return ExecuteAsync<IList<OnlineNotification>>(HttpMethod.Get, "/api/v1/online_notifications");
        }

        public Task<IList<OnlineNotification>> GetOnlineNotificationListAsync(int page, int count)
        {
            return ExecuteAsync<IList<OnlineNotification>>(HttpMethod.Get, "/api/v1/online_notifications?page={page},per_page={count}");
        }

        public Task<OnlineNotification> GetOnlineNotificationAsync(int id)
        {
            return ExecuteAsync<OnlineNotification>(HttpMethod.Get, "/api/v1/online_notifications/{id}");
        }

        public Task<OnlineNotification> CreateOnlineNotificationAsync(OnlineNotification notification)
        {
            return ExecuteAsync<OnlineNotification>(HttpMethod.Post, "/api/v1/online_notifications", notification);
        }

        public Task<OnlineNotification> UpdateOnlineNotificationAsync(int id, OnlineNotification notification)
        {
            return ExecuteAsync<OnlineNotification>(HttpMethod.Put, "/api/v1/online_notifications/{id}", notification);
        }

        public Task<bool> DeleteOnlineNotificationAsync(int id)
        {
            return ExecuteAsync<bool>(HttpMethod.Delete, "/api/v1/online_notifications/{id}");
        }

        public Task<bool> MarkAllAsReadAsync()
        {
            return ExecuteAsync<bool>(HttpMethod.Post, "/api/v1/online_notifications/mark_all_as_read");
        }

        #endregion
    }
}

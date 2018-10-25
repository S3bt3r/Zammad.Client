using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Core;
using Zammad.Client.Resources;
using Zammad.Client.Services;

namespace Zammad.Client
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
            return GetAsync<IList<OnlineNotification>>("/api/v1/online_notifications");
        }

        public Task<IList<OnlineNotification>> GetOnlineNotificationListAsync(int page, int count)
        {
            return GetAsync<IList<OnlineNotification>>("/api/v1/online_notifications", $"page={page}&per_page={count}");
        }

        public Task<OnlineNotification> GetOnlineNotificationAsync(int id)
        {
            return GetAsync<OnlineNotification>($"/api/v1/online_notifications/{id}");
        }

        public Task<OnlineNotification> CreateOnlineNotificationAsync(OnlineNotification notification)
        {
            return PostAsync<OnlineNotification>("/api/v1/online_notifications", notification);
        }

        public Task<OnlineNotification> UpdateOnlineNotificationAsync(int id, OnlineNotification notification)
        {
            return PutAsync<OnlineNotification>($"/api/v1/online_notifications/{id}", notification);
        }

        public Task<bool> DeleteOnlineNotificationAsync(int id)
        {
            return DeleteAsync<bool>($"/api/v1/online_notifications/{id}");
        }

        public Task<bool> MarkAllAsReadAsync()
        {
            return PostAsync<bool>("/api/v1/online_notifications/mark_all_as_read");
        }

        #endregion
    }
}

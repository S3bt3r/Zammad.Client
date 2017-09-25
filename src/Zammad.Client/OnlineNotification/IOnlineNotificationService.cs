using System.Collections.Generic;
using System.Threading.Tasks;

namespace Zammad.Client.OnlineNotification
{
    public interface IOnlineNotificationService
    {
        Task<IList<OnlineNotification>> GetOnlineNotificationListAsync();
        Task<IList<OnlineNotification>> GetOnlineNotificationListAsync(int page, int count);
        Task<OnlineNotification> GetOnlineNotificationAsync(int id);
        Task<OnlineNotification> CreateOnlineNotificationAsync(OnlineNotification notification);
        Task<OnlineNotification> UpdateOnlineNotificationAsync(int id, OnlineNotification notification);
        Task<bool> DeleteOnlineNotificationAsync(int id);
        Task<bool> MarkAllAsReadAsync();
    }
}

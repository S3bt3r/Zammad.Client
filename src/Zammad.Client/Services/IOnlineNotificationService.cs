using System.Collections.Generic;
using System.Threading.Tasks;
using Zammad.Client.Resources;

namespace Zammad.Client.Services
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

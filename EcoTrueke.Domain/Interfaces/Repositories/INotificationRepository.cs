using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(Notification notification);
        Task DeleteNotification(string notificationId);
        Task MarkAsRead(IEnumerable<string> notificationIds);
    }
}

using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> CreateNotification(Notification notification);
    }
}

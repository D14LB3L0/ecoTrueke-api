using EcoTrueke.Domain.Entities;

namespace EcoTrueke.Domain.Interfaces.Queries
{
    public interface INotificationQuery
    {
        Task<(List<Notification> Notifications, int totalPages)> GetPaginatedNotifications(int page, int amountPage, string loggedUserId);
    }
}

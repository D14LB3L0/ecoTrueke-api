using EcoTrueke.Services.API;

namespace EcoTrueke.Domain.Interfaces.UseCases.Notification
{
    public interface INotificationUseCase
    {
        Task<Result> GetPaginatedNotificationsExecute(int page, int amountPage, string loggedUserId);

        Task<Result> MarkAsReadExecute(IEnumerable<string> notificationsIds);
    }
}

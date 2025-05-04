using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.UseCases.Notification;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Notification
{
    public class NotificationUseCase : INotificationUseCase
    {
        private readonly INotificationQuery _notificationQuery;

        public NotificationUseCase(INotificationQuery notificationQuery)
        {
            _notificationQuery = notificationQuery;
        }

        public async Task<Result> GetPaginatedNotificationsExecute(int page, int amountPage, string loggedUserId)
        {
            var (notifications, totalPages) = await _notificationQuery.GetPaginatedNotifications(page, amountPage, loggedUserId);

            var paginationResponse = new GetPaginatedNotificationsResponse(notifications, totalPages);

            return new Result { Code = Success.Notification.GetPaginatedNotifications.Code, Data = JsonConvert.SerializeObject(paginationResponse), Message = Success.Notification.GetPaginatedNotifications.Message };
        }
    }
}

using EcoTrueke.Domain.Constants;
using EcoTrueke.Domain.Interfaces.Queries;
using EcoTrueke.Domain.Interfaces.Repositories;
using EcoTrueke.Domain.Interfaces.UseCases.Notification;
using EcoTrueke.Services.API;
using Newtonsoft.Json;

namespace EcoTrueke.Application.UseCases.Notification
{
    public class NotificationUseCase : INotificationUseCase
    {
        private readonly INotificationQuery _notificationQuery;
        private readonly INotificationRepository _notificationRepository;

        public NotificationUseCase(INotificationQuery notificationQuery, INotificationRepository notificationRepository)
        {
            _notificationQuery = notificationQuery;
            _notificationRepository = notificationRepository;
        }

        public async Task<Result> DeleteNotificationExecute(string notifiationId)
        {
            try
            {
                await _notificationRepository.DeleteNotification(notifiationId);

                return new Result { Code = Success.Notification.DeleteNotification.Code, Message = Success.Notification.DeleteNotification.Message };
            }
            catch (Exception)
            {
                return new Result { Code = Errors.Notification.FailedDelete.Code, Message = Errors.Notification.FailedDelete.Message };
            }

        }

        public async Task<Result> GetPaginatedNotificationsExecute(int page, int amountPage, string loggedUserId)
        {
            var (notifications, totalPages) = await _notificationQuery.GetPaginatedNotifications(page, amountPage, loggedUserId);

            var paginationResponse = new GetPaginatedNotificationsResponse(notifications, totalPages);

            return new Result { Code = Success.Notification.GetPaginatedNotifications.Code, Data = JsonConvert.SerializeObject(paginationResponse), Message = Success.Notification.GetPaginatedNotifications.Message };
        }

        public async Task<Result> MarkAsReadExecute(IEnumerable<string> notificationIds)
        {
            try
            {
                await _notificationRepository.MarkAsRead(notificationIds);

                return new Result { Code = Success.Notification.MarkAsRead.Code, Message = Success.Notification.MarkAsRead.Message };
            }
            catch (Exception)
            {
                return new Result { Code = Errors.Notification.FailedMarkAsRead.Code, Message = Errors.Notification.FailedMarkAsRead.Message };
            }
        }
    }
}

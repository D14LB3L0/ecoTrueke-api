namespace EcoTrueke.API.Responses.Notification
{
    public class GetPaginatedNotificationsResponse
    {
        public List<NotificationsResponse> Notifications { get; set; }
        public int TotalPages { get; set; }

        public GetPaginatedNotificationsResponse(List<Domain.Entities.Notification> notifications, int totalPages)
        {
            Notifications = new();
            foreach (var notifiaction in notifications)
                Notifications.Add(new NotificationsResponse(notifiaction));
            TotalPages = totalPages;
        }

        public class NotificationsResponse
        {
            public string Id { get; set; }
            public string Title { get; set; }
            public string Message { get; set; }
            public string Type { get; set; }
            public NotificationsResponse(Domain.Entities.Notification notification)
            {
                Id = notification.Id;
                Title = notification.Title;
                Message = notification.Message;
                Type = notification.Type;
            }
        }
    }
}


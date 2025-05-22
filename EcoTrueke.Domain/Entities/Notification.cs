using EcoTrueke.Domain.Constants;

namespace EcoTrueke.Domain.Entities
{
    public class Notification
    {
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Message { get; set; }

        public string Type { get; set; } // info - alert - promo

        public bool IsRead { get; set; }

        public string Link { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public static Notification FinishSetup(string userId)
        {
            return new Notification
            {
                UserId = userId,
                Title = Notifications.FinishSetup.Title,
                Message = Notifications.FinishSetup.Message, 
                Type = Notifications.FinishSetup.Type,
                IsRead = false,
                Link = Notifications.FinishSetup.Link,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
        }       
        
        public static Notification ExchangeRequest(string userId)
        {
            return new Notification
            {
                UserId = userId,
                Title = Notifications.ExchangeRequest.Title,
                Message = Notifications.ExchangeRequest.Message, 
                Type = Notifications.ExchangeRequest.Type,
                IsRead = false,
                Link = Notifications.ExchangeRequest.Link,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
        }
    }
}

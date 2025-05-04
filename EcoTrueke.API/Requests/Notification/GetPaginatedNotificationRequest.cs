using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Notification
{
    public class GetPaginatedNotificationsRequest
    {
        [Required]
        public int Page { get; set; }

        [Required]
        public int AmountPage { get; set; }
    }
}

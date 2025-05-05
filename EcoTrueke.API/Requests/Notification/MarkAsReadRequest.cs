using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Notification
{
    public class MarkAsReadRequest
    {
        [Required]
        public IEnumerable<string> NotificationIds { get; set; }
    }
}

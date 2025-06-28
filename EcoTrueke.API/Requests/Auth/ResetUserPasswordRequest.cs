using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Auth
{
    public class ResetUserPasswordRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

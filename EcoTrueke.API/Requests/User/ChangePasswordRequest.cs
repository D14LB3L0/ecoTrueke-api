using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.User
{
    public class ChangePasswordRequest
    {
        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.User
{
    public class ChangePasswordRequest
    {
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string Password { get; set; }
    }
}

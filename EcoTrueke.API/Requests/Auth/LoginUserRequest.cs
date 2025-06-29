using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Auth
{
    public class LoginUserRequest
    {
        [Required]
        [EmailAddress]      
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo no tiene un formato válido.")]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

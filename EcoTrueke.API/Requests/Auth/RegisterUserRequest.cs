using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Auth
{
    public class RegisterUserRequest
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]

        public string PaternalSurname { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]

        public string MaternalSurname { get; set; }

        [Required]
        [EmailAddress]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "El correo no tiene un formato válido.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]

        public string Password { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]

        public string ConfirmPassword { get; set; }
    }
}

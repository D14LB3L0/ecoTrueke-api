using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Auth
{
    public class RegisterUserRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]

        public string PaternalSurname { get; set; }

        [Required]

        public string MaternalSurname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]

        public string Password { get; set; }

        [Required]

        public string ConfirmPassword { get; set; }
    }
}

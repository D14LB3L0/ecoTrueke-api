using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Person
{
    public class EditPersonRequest
    {
        public IFormFile? ProfilePicture { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        public string? ProfilePictureRemove { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string PaternalSurname { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string MaternalSurname { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string Phone { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        public string? Address { get; set; } = null;
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string DocumentNumber { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string DocumentType { get; set; }    // "dni"
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        public string? Gender { get; set; } = null;  // "female", "male", "other"
    }
}

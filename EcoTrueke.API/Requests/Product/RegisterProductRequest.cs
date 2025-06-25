using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Product
{
    public class RegisterProductRequest
    {
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        public IFormFile? ProductPicture { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        public string? Description { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string TypeTranscription { get; set; } // exchange - donation - sale
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public IEnumerable<string> Category { get; set; }  // clothes - toys
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string Condition { get; set; }
        [RegularExpression(@"^[a-zA-Z0-9\s@]+$", ErrorMessage = "Solo se permiten letras, números, espacios y el símbolo '@'.")]
        [Required]
        public string Quantity { get; set; }
    }
}

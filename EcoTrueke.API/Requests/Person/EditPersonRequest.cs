using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Person
{
    public class EditPersonRequest
    {
        public IFormFile? ProfilePicture { get; set; }

        public string? ProfilePictureRemove { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PaternalSurname { get; set; }

        [Required]
        public string MaternalSurname { get; set; }

        [Required]
        public string Phone { get; set; }

        public string? Address { get; set; } = null;

        [Required]
        public string DocumentNumber { get; set; }

        [Required]
        public string DocumentType { get; set; }    // "dni"

        public string? Gender { get; set; } = null;  // "female", "male", "other"
    }
}

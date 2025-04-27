using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Person
{
    public class EditPersonRequest
    {
        [Required]
        public string name { get; set; }

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

        //[Required]
        //public string ProfilePicture { get; set; }
    }
}

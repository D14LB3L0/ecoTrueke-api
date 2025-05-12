using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Product
{
    public class RegisterProductRequest
    {
        public IFormFile? ProductPicture { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public string TypeTranscription { get; set; } // exchange - donation - sale

        [Required]
        public IEnumerable<string> Category { get; set; }  // clothes - toys

        [Required]
        public string Condition { get; set; }  
        
        [Required]
        public string Quantity { get; set; }
    }
}

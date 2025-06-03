using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.UserRating
{
    public class RatingUserRequest
    {
        [Required]
        public string QualifiedUserId { get; set; }

        [Required]
        public int Stars { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.UserRating
{
    public class RatingUserRequest
    {
        [Required]
        public string ProposerUserId { get; set; }        
        
        [Required]
        public string ProposalId { get; set; }

        [Required]
        public int Stars { get; set; }
    }
}

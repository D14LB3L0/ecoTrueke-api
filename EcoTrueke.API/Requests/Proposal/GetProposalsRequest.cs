using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Proposal
{
    public class GetProposalsRequest
    {
        [Required]
        public int Page { get; set; }

        [Required]
        public int AmountPage { get; set; }

        public string? Status { get; set; }
    }
}

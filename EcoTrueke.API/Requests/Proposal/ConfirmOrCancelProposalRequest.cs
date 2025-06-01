using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Proposal
{
    public class ConfirmOrCancelProposalRequest
    {
        [Required]
        public string ProposalId { get; set; }

        [Required]
        public string ProductAction { get; set; } 
        
        [Required]
        public string ProposalAction { get; set; }
    }
}

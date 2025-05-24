using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Proposal
{
    public class RejectOrAcceptProposalRequest
    {
        [Required]
        public string ProposalId { get; set; }
        [Required]
        public string Action {  get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace EcoTrueke.API.Requests.Proposal
{
    public class ExchangeProposalRequest
    {
        [Required]
        public string OwnerId {  get; set; }

        [Required]
        public string ProposalType { get; set; }

        [Required]
        public string OfferedProductId { get; set; }

        [Required]
        public string RequestedProductId { get; set; }
    }
}

namespace EcoTrueke.API.Responses.Proposal
{
    public class GetProposalsResponse
    {
        public List<ProposalResponse> Proposals { get; set; }

        public GetProposalsResponse(List<Domain.Entities.Proposal> proposals)
        {
            Proposals = new();
            foreach (var proposal in proposals)
            {
                Proposals.Add(new ProposalResponse(proposal));
            }
        }
    }

    public class ProposalResponse
    {
        public string Id { get; set; }

        public string ProposerId { get; set; }

        public string OwnerId { get; set; }

        public string ProposalType { get; set; }

        public string OfferedProductId { get; set; }

        public string RequestedProductId { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ProposalResponse(Domain.Entities.Proposal proposal)
        {
            Id = proposal.Id;
            ProposerId = proposal.ProposerId;
            OwnerId = proposal.OwnerId;
            ProposalType = proposal.ProposalType;
            OfferedProductId = proposal.OfferedProductId;
            RequestedProductId = proposal.RequestedProductId;
            Status = proposal.Status;
            CreatedAt = proposal.CreatedAt;
        }
    }
}

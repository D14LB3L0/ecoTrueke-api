using static EcoTrueke.Domain.Constants.Types;

namespace EcoTrueke.Domain.Entities
{
    public class Proposal
    {
        public string Id { get; set; }

        public string ProposerId { get; set; }

        public string OwnerId { get; set; }

        public string ProposalType { get; set; }

        public string OfferedProductId { get; set; }

        public string RequestedProductId { get; set; }

        public string Status { get; set; }

        public DateTime UpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public bool IsDeleted { get; set; }
    
        public static Proposal Create(string proposerId, string ownerId, string proposalType, string offeredProductId, string requestedProductId)
        {
            return new()
            {
                ProposerId = proposerId,
                OwnerId = ownerId,
                ProposalType = proposalType,
                OfferedProductId = offeredProductId,
                RequestedProductId = requestedProductId,
                Status = ProposalStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsDeleted = false
            };
        }
    }
}

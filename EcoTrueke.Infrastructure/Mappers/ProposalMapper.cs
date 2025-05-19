namespace EcoTrueke.Infrastructure.Mappers
{
    public static class ProposalMapper
    {
        public static MongoModels.Proposal ToMongo(Domain.Entities.Proposal proposal)
        {
            return new MongoModels.Proposal
            {
                Id = proposal.Id,
                ProposerId = proposal.ProposerId,
                OwnerId = proposal.OwnerId,
                ProposalType = proposal.ProposalType,
                OfferedProductId = proposal.OfferedProductId,
                RequestedProductId = proposal.RequestedProductId,
                Status = proposal.Status,
                UpdatedAt = proposal.UpdatedAt,
                CreatedAt = proposal.CreatedAt,
                IsDeleted = proposal.IsDeleted,
            };
        }

        public static Domain.Entities.Proposal ToDomain(MongoModels.Proposal mongoProposal)
        {
            return new Domain.Entities.Proposal
            {
                Id = mongoProposal.Id,
                ProposerId = mongoProposal.ProposerId,
                OwnerId = mongoProposal.OwnerId,
                ProposalType= mongoProposal.ProposalType,
                OfferedProductId= mongoProposal.OfferedProductId,
                RequestedProductId= mongoProposal.RequestedProductId,
                Status = mongoProposal.Status,
                UpdatedAt = mongoProposal.UpdatedAt,
                CreatedAt = mongoProposal.CreatedAt,
                IsDeleted = mongoProposal.IsDeleted,
            };
        }
    }
}

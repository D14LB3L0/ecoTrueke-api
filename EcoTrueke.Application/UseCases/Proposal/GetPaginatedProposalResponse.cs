namespace EcoTrueke.Application.UseCases.Proposal
{
    public class GetPaginatedProposalResponse
    {
        public List<ProposalResponse> Proposals { get; set; }
        public int TotalPages { get; set; }

        public GetPaginatedProposalResponse(List<Domain.DTOs.GetProposalResponse> proposals, int totalPages)
        {
            Proposals = new();
            foreach (var proposal in proposals)
                Proposals.Add(new ProposalResponse(proposal));

            TotalPages = totalPages;
        }

        public class ProposalResponse
        {
            public string Id { get; set; }
            public string ProposerId { get; set; }
            public string OwnerId { get; set; }
            public string ProposalType { get; set; }
            public string Status { get; set; }
            public DateTime CreatedAt { get; set; }

            public UserDto ProposerUser { get; set; }
            public PersonDto ProposerPerson { get; set; }
            public ProductDto OfferedProduct { get; set; }
            public ProductDto RequestedProduct { get; set; }

            public ProposalResponse(Domain.DTOs.GetProposalResponse proposal)
            {
                Id = proposal.Id;
                ProposerId = proposal.ProposerId;
                OwnerId = proposal.OwnerId;
                ProposalType = proposal.ProposalType;
                Status = proposal.Status;
                CreatedAt = proposal.CreatedAt;

                ProposerUser = new UserDto
                {
                    Id = proposal.ProposerUser.Id
                };

                ProposerPerson = new PersonDto
                {
                    Id = proposal.ProposerPerson.Id,
                    Name = proposal.ProposerPerson.Name
                };

                OfferedProduct = new ProductDto
                {
                    Id = proposal.OfferedProduct.Id,
                    Name = proposal.OfferedProduct.Name,
                    ProductPicture = proposal.OfferedProduct.ProductPicture
                };

                RequestedProduct = new ProductDto
                {
                    Id = proposal.RequestedProduct.Id,
                    Name = proposal.RequestedProduct.Name,
                    ProductPicture = proposal.RequestedProduct.ProductPicture

                };
            }
        }

        public class UserDto
        {
            public string Id { get; set; }
        }

        public class PersonDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class ProductDto
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string ProductPicture { get; set; }
        }
    }
}

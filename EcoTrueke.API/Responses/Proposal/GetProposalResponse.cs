namespace EcoTrueke.API.Responses.Proposal
{
    public class GetPaginatedProposalResponse
    {
        public List<GetProposalResponse> Proposals { get; set; }
        public int TotalPages { get; set; }
    }

    public class GetProposalResponse
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

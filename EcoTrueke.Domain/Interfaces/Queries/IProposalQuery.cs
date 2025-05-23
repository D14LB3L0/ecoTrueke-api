    namespace EcoTrueke.Domain.Interfaces.Queries
{
    public interface IProposalQuery
    {
        Task<(List<Object> Proposals, int totalPages)> GetProposals(int page, int amountPage, string loggedUserId);
    }
}

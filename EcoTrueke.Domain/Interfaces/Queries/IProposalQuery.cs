using EcoTrueke.Domain.DTOs;

namespace EcoTrueke.Domain.Interfaces.Queries
{
    public interface IProposalQuery
    {
        Task<(List<GetProposalResponse> proposals, int totalPages)> GetProposals(int page, int amountPage, string status,string loggedUserId);
    }
}

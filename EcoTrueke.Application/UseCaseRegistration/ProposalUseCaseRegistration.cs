using EcoTrueke.Application.UseCases.Proposal;
using EcoTrueke.Domain.Interfaces.UseCases.Proposal;
using Microsoft.Extensions.DependencyInjection;

namespace EcoTrueke.Application.UseCaseRegistration
{
    public static class ProposalUseCaseRegistration
    {
        public static IServiceCollection AddProposalUseCases(this IServiceCollection services)
        {
            services.AddScoped<IProposalUseCase, ProposalUseCase>();

            return services;
        }
    }
}

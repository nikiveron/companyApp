using AutoMapper;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Services.Interfaces;
using FluentValidation;
using MediatR;

namespace companyApp.Server.Services.Features;

public class GetAgent
{
    // Input 
    public class Query(int id) : IRequest<ReadAgentDTO?>
    {
        public int Id { get; set; } = id;
    }
    // Validator
    public class GetAgentValidator : AbstractValidator<Query>
    {
        public GetAgentValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id агента обязательно.");
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id агента должно быть натуральным числом.");
        }
    }

    //Handler
    public class GetAgentHandler(IAgentRepository AgentRepository) : IRequestHandler<Query, ReadAgentDTO?>
    {
        public async Task<ReadAgentDTO?> Handle(Query request, CancellationToken cancellationToken)
        {
            return await AgentRepository.Get(request.Id, cancellationToken);
        }
    }
}

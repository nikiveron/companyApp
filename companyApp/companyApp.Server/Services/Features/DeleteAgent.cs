using AutoMapper;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Services.Interfaces;
using FluentValidation;
using MediatR;

namespace companyApp.Server.Services.Features;

public class DeleteAgent
{
    // Input 
    public class Query(int id) : IRequest<bool>
    {
        public int Id { get; set; } = id;
    }
    // Validator
    public class DeleteAgentValidator : AbstractValidator<Query>
    {
        public DeleteAgentValidator()
        {
            RuleFor(x => x.Id).NotNull().WithMessage("Id агента обязательно.");
            RuleFor(x => x.Id).InclusiveBetween(1, int.MaxValue).WithMessage("Id агента должно быть натуральным числом.");
        }
    }

    //Handler
    public class DeleteAgentHandler(IAgentRepository AgentRepository, IUriService uriService, IMapper mapper) : IRequestHandler<Query, bool>
    {
        public async Task<bool> Handle(Query request, CancellationToken cancellationToken)
        {
            return await AgentRepository.Delete(request.Id, cancellationToken);
        }
    }
}

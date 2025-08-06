using AutoMapper;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Services.Interfaces;
using FluentValidation;
using MediatR;

namespace companyApp.Server.Services.Features;

public class UpdateAgent
{
    // Input 
    public class Query(UpdateAgentDTO agentDTO) : IRequest
    {
        public UpdateAgentDTO Agent { get; set; } = agentDTO;
    }
    // Validator
    public class UpdateAgentValidator : AbstractValidator<Query>
    {
        public UpdateAgentValidator()
        {
            RuleFor(x => x.Agent).NotNull().WithMessage("Данные агента обязательны.");
            When(x => x.Agent != null, () =>
            {
                RuleFor(x => x.Agent.ShortName)
                    .NotEmpty().WithMessage("Краткое название компании обязательно");

                RuleFor(x => x.Agent.FullName)
                    .NotEmpty().WithMessage("Полное название компании обязательно");

                RuleFor(x => x.Agent.Inn)
                    .NotEmpty().WithMessage("ИНН обязателен");

                RuleFor(x => x.Agent.Kpp)
                    .NotEmpty().WithMessage("КПП обязателен");

                RuleFor(x => x.Agent.Ogrn)
                    .NotEmpty().WithMessage("ОГРН обязателен");

                RuleFor(x => x.Agent.OgrnDateOfIssue)
                    .NotEmpty().WithMessage("Дата выдачи ОГРН обязательна");

                RuleFor(x => x.Agent.RepLastName)
                    .NotEmpty().WithMessage("Фамилия представителя обязательна");

                RuleFor(x => x.Agent.RepFirstName)
                    .NotEmpty().WithMessage("Имя представителя обязательно");

                RuleFor(x => x.Agent.RepEmail)
                    .NotEmpty().WithMessage("Email представителя обязателен")
                    .EmailAddress().WithMessage("Некорректный формат email");

                RuleFor(x => x.Agent.RepPhone)
                    .NotEmpty().WithMessage("Телефон представителя обязателен");
            });
        }
    }

    //Handler
    public class UpdateAgentHandler(IAgentRepository AgentRepository, IUriService uriService, IMapper mapper) : IRequestHandler<Query>
    {
        public async Task Handle(Query request, CancellationToken cancellationToken)
        {
            await AgentRepository.Update(request.Agent, cancellationToken);
        }
    }
}

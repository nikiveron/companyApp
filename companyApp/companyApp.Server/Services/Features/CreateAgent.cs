using AutoMapper;
using companyApp.Server.Filters;
using companyApp.Server.Filters.Pagination;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Services.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace companyApp.Server.Services.Features;

public class CreateAgent
{
    // Input 
    public class Query(CreateAgentDTO agent) : IRequest<int>
    {
        public CreateAgentDTO Agent { get; set; } = agent;
    }
    // Validator
    public class CreateAgentValidator : AbstractValidator<Query>
    {
        public CreateAgentValidator()
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
    public class CreateAgentHandler(IAgentRepository AgentRepository, IUriService uriService, IMapper mapper) : IRequestHandler<Query, int>
    {
        public async Task<int> Handle(Query request, CancellationToken cancellationToken)
        {
            return await AgentRepository.Create(request.Agent, cancellationToken);
        }
    }
}

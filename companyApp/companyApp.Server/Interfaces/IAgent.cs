using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace companyApp.Server.Interfaces;

public interface IAgentRepository
{
    Task<IEnumerable<AgentDTO>> Get(CancellationToken cancellationToken);
    Task<AgentDTO?> Get(int agentId, CancellationToken cancellationToken);
    Task<int> Create(CreateAgentDTO agentDTO, CancellationToken cancellationToken);
    void Update(AgentDTO agentDTO, CancellationToken cancellationToken);
    Task<AgentDTO?> Delete(int agentId, CancellationToken cancellationToken);
}

public class AgentRepository(ApplicationContext context) : IAgentRepository
{
    public async Task<IEnumerable<AgentDTO>> Get(CancellationToken cancellationToken)
    {
        var agents = await context.Agents
            .OrderBy(a => a.AgentId)
            .Select(a => new AgentDTO
            {
                AgentId = a.AgentId,
                RepLastName = a.Company.RepLastName,
                RepFirstName = a.Company.RepFirstName,
                RepPatronymic = a.Company.RepPatronymic,
                RepEmail = a.Company.RepEmail,
                RepPhone = a.Company.RepPhone,
                ShortName = a.Company.ShortName,
                FullName = a.Company.FullName,
                Inn = a.Company.Inn,
                Kpp = a.Company.Kpp,
                Ogrn = a.Company.Ogrn,
                OgrnDateOfIssue = a.Company.OgrnDateOfIssue,
                Banks = a.Banks.Select(b => new BankDTO
                {
                    BankId = b.BankId,
                    ShortName = b.Company.ShortName
                }).ToList(),
                Priority = a.Priority
            })
            .Take(10)   // убрать потом
            .ToListAsync(cancellationToken);
        return agents;
    }
    public async Task<AgentDTO?> Get(int Id, CancellationToken cancellationToken)
    {
        var a = await context.Agents
            .Include(agent => agent.Company)
            .Include(agent => agent.Banks)
                .ThenInclude(bank => bank.Company)
            .FirstOrDefaultAsync(agent => agent.AgentId == Id, cancellationToken);

        if (a == null || a.Company == null)
            return null;

        AgentDTO agent = new()
        {
            AgentId = a.AgentId,
            RepLastName = a.Company.RepLastName,
            RepFirstName = a.Company.RepFirstName,
            RepPatronymic = a.Company.RepPatronymic,
            RepEmail = a.Company.RepEmail,
            RepPhone = a.Company.RepPhone,
            ShortName = a.Company.ShortName,
            FullName = a.Company.FullName,
            Inn = a.Company.Inn,
            Kpp = a.Company.Kpp,
            Ogrn = a.Company.Ogrn,
            OgrnDateOfIssue = a.Company.OgrnDateOfIssue,
            Banks = a.Banks?.Select(b => new BankDTO
            {
                BankId = b.BankId,
                ShortName = b.Company.ShortName
            }).ToList() ?? [],
            Priority = a.Priority
        };
        return agent;
    }
    public async Task<int> Create(CreateAgentDTO agent, CancellationToken cancellationToken)
    {
        await CreateAgentDTOCheck.UniqueAgentCheck(context, agent, cancellationToken);
        var agentEntity = new AgentEntity
        {
            DeletedAt = null,
            Banks = agent.Banks?.Select(bank => context.Banks.FirstOrDefault(b => b.BankId == bank))
                .Where(bank => bank != null)
                .ToList() ?? [],
            Company = await context.Companies.FirstOrDefaultAsync(c => c.Inn == agent.Inn, cancellationToken) ?? new CompanyEntity
            {
                ShortName = agent.ShortName,
                FullName = agent.FullName,
                Inn = agent.Inn,
                Kpp = agent.Kpp,
                Ogrn = agent.Ogrn,
                OgrnDateOfIssue = agent.OgrnDateOfIssue,
                RepLastName = agent.RepLastName,
                RepFirstName = agent.RepFirstName,
                RepPatronymic = agent.RepPatronymic,
                RepEmail = agent.RepEmail,
                RepPhone = agent.RepPhone,
                DeletedAt = null
            },
            Priority = agent.Priority
        };
        context.Agents.Add(agentEntity);
        await context.SaveChangesAsync(cancellationToken);
        
        return await context.Agents
            .Select(a => a.AgentId)
            .FirstOrDefaultAsync(a => a == agentEntity.AgentId, cancellationToken);
    }
    public async void Update(AgentDTO updatedTodoItem, CancellationToken cancellationToken) // в разработке
    {
        //AgentDTO currentItem = await Get(updatedTodoItem.Id);
        //currentItem.IsComplete = updatedTodoItem.IsComplete;
        //currentItem.TaskDescription = updatedTodoItem.TaskDescription;

        //await context.TodoItems.Update(currentItem);
        context.SaveChanges();
    }

    public async Task<AgentDTO> Delete(int Id, CancellationToken cancellationToken) // в разработке
    {
        AgentDTO agentDTO = await Get(Id, cancellationToken);

        if (agentDTO != null)
        {
            //await context.TodoItems.Remove(agentDTO);
            context.SaveChanges();
        }

        return agentDTO;
    }
}


using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace companyApp.Server.Interfaces;

public interface IAgentRepository
{
    Task<IEnumerable<AgentDTO>> Get(CancellationToken cancellationToken);
    Task<AgentDTO?> Get(int agentId, CancellationToken cancellationToken);
    Task<int> Create(CreateAgentDTO agentDTO, CancellationToken cancellationToken);
    Task Update(PutAgentDTO agentDTO, CancellationToken cancellationToken);
    Task<bool> Delete(int agentId, CancellationToken cancellationToken);
}

public class AgentRepository(ApplicationContext context) : IAgentRepository
{
    public async Task<IEnumerable<AgentDTO>> Get(CancellationToken cancellationToken)
    {
        var agents = await context.Agents
            .OrderBy(a => a.AgentId)
            .Where(a => a.DeletedAt == null)
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

        if (a == null || a.Company == null || a.DeletedAt != null)
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
    public async Task Update(PutAgentDTO agent, CancellationToken cancellationToken)
    {
        await PutAgentDTOCheck.UniqueAgentCheck(context, agent, cancellationToken);
        var currentAgent = await context.Agents
            .Include(a => a.Company)
            .Include(a => a.Banks)
            .FirstOrDefaultAsync(a => a.AgentId == agent.Id && a.DeletedAt == null, cancellationToken);
            
        if (currentAgent == null)
        {
            throw new Exception("Агент не найден!");
        }
        currentAgent.Company.RepLastName = agent.RepLastName;
        currentAgent.Company.RepFirstName = agent.RepFirstName;
        currentAgent.Company.RepPatronymic = agent.RepPatronymic;
        currentAgent.Company.RepEmail = agent.RepEmail;
        currentAgent.Company.RepPhone = agent.RepPhone;
        currentAgent.Company.ShortName = agent.ShortName;
        currentAgent.Company.FullName = agent.FullName;
        currentAgent.Company.Inn = agent.Inn;
        currentAgent.Company.Kpp = agent.Kpp;
        currentAgent.Company.Ogrn = agent.Ogrn;
        currentAgent.Company.OgrnDateOfIssue = agent.OgrnDateOfIssue;
        currentAgent.Banks = agent.Banks
            .Select(bankId => context.Banks.FirstOrDefault(b => b.BankId == bankId))
            .Where(bank => bank != null)
            .ToList();
        currentAgent.Priority = agent.Priority;

        await context.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> Delete(int Id, CancellationToken cancellationToken)
    {
        var agent = await context.Agents.FirstOrDefaultAsync(a => a.AgentId == Id && a.DeletedAt == null, cancellationToken);
        if (agent != null)
        {
            agent.DeletedAt = DateTime.UtcNow;
            await context.SaveChangesAsync(cancellationToken);
            return true;
        }

        return false;
    }
}


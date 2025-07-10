using AutoMapper;
using companyApp.Server.Mapping;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Interfaces;

public interface IAgentRepository
{
    Task<IEnumerable<ReadAgentDTO>> Get(IMapper _mapper,CancellationToken cancellationToken);
    Task<ReadAgentDTO?> Get(int agentId, IMapper _mapper, CancellationToken cancellationToken);
    Task<int> Create(CreateAgentDTO agentDTO, IMapper _mapper,CancellationToken cancellationToken);
    Task Update(UpdateAgentDTO agentDTO, IMapper mapper, CancellationToken cancellationToken);
    Task<bool> Delete(int agentId, CancellationToken cancellationToken);
}

public class AgentRepository(ApplicationContext context) : IAgentRepository
{
    public async Task<IEnumerable<ReadAgentDTO>> Get(IMapper _mapper,CancellationToken cancellationToken)
    {
        var agents = await context.Agents
            .Include(a => a.Company)
            .Include(agent => agent.Banks)
                .ThenInclude(bank => bank.Company)
            .OrderBy(a => a.AgentId)
            .Where(a => a.DeletedAt == null)
            .Take(10)   // убрать потом
            .ToListAsync(cancellationToken);

        return agents.Select(a => _mapper.Map<ReadAgentDTO>(a));
    }
    public async Task<ReadAgentDTO?> Get(int Id, IMapper _mapper, CancellationToken cancellationToken)
    {
        var agent = await context.Agents
            .Include(agent => agent.Company)
            .Include(agent => agent.Banks)
                .ThenInclude(bank => bank.Company)
            .FirstOrDefaultAsync(agent => agent.AgentId == Id, cancellationToken);

        if (agent == null || agent.Company == null || agent.DeletedAt != null)
            return null;

        return _mapper.Map<ReadAgentDTO>(agent);
    }
    public async Task<int> Create(CreateAgentDTO agent, IMapper _mapper,CancellationToken cancellationToken)
    {
        await CreateAgentDTOCheck.UniqueAgentCheck(context, agent, cancellationToken);
        AgentEntity agentEntity = _mapper.Map<AgentEntity>(agent);

        agentEntity.Banks = agent.Banks?.Select(bank => context.Banks.FirstOrDefault(b => b.BankId == bank))
                .Where(bank => bank != null)
                .ToList() ?? [];
        agentEntity.Company = await context.Companies.FirstOrDefaultAsync(c => c.Inn == agent.Inn, cancellationToken) ?? _mapper.Map<CompanyEntity>(agentEntity);
        context.Agents.Add(agentEntity);
        await context.SaveChangesAsync(cancellationToken);
        return await context.Agents
            .Select(a => a.AgentId)
            .FirstOrDefaultAsync(a => a == agentEntity.AgentId, cancellationToken);
    }
    public async Task Update(UpdateAgentDTO agent, IMapper mapper, CancellationToken cancellationToken)
    {
        await UpdateAgentDTOCheck.UniqueAgentCheck(context, agent, cancellationToken);
        var currentAgent = await context.Agents
            .Include(a => a.Company)
            .Include(a => a.Banks)
            .FirstOrDefaultAsync(a => a.AgentId == agent.Id && a.DeletedAt == null, cancellationToken);
            
        if (currentAgent == null)
        {
            throw new Exception("Агент не найден!");
        }

        mapper.Map(agent, currentAgent);
        currentAgent.Banks = agent.Banks
            .Select(bankId => context.Banks.FirstOrDefault(b => b.BankId == bankId))
            .Where(bank => bank != null)
            .ToList();

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


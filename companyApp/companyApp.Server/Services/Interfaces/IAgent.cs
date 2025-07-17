using AutoMapper;
using companyApp.Server.Filters;
using companyApp.Server.Filters.Pagination;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Services.Interfaces;

public interface IAgentRepository
{
    Task<Tuple<int, List<ReadAgentDTO>>> Get(InfoFilter infoFilter, PaginationFilter validFilter, CancellationToken cancellationToken);
    Task<ReadAgentDTO?> Get(int agentId, CancellationToken cancellationToken);
    Task<int> Create(CreateAgentDTO agentDTO,CancellationToken cancellationToken);
    Task Update(UpdateAgentDTO agentDTO, CancellationToken cancellationToken);
    Task<bool> Delete(int agentId, CancellationToken cancellationToken);
}

public class AgentRepository(ApplicationContext context, IMapper _mapper) : IAgentRepository
{
    public async Task<Tuple<int, List<ReadAgentDTO>>> Get(InfoFilter infoFilter, PaginationFilter validFilter, CancellationToken cancellationToken)
    {
        var queryBase = context.Agents
            .Include(a => a.Company)
            .Include(agent => agent.Banks)
                .ThenInclude(bank => bank.Company)
            .OrderBy(a => a.AgentId)
            .Where(a => a.Company.Inn.Contains(infoFilter.Inn)   // filters 
                     && a.Company.RepPhone.Contains(infoFilter.PhoneNumber)
                     && a.Company.RepEmail.Contains(infoFilter.Email)
                     && a.Company.OgrnDateOfIssue > infoFilter.OgrnFrom
                     && a.Company.OgrnDateOfIssue < infoFilter.OgrnTo)
            .Where(a => a.DeletedAt == null && (!infoFilter.Priority || a.Priority));
        var totalRecords = await queryBase.CountAsync(cancellationToken);
        var agents = await PaginationHelper.ApplyPagination(queryBase, validFilter, cancellationToken);
        var response = agents.Select(a => _mapper.Map<ReadAgentDTO>(a)).ToList();
        Tuple<int, List<ReadAgentDTO>> result = new(totalRecords, response);
        return result;
    }
    public async Task<ReadAgentDTO?> Get(int Id, CancellationToken cancellationToken)
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
    public async Task<int> Create(CreateAgentDTO agent, CancellationToken cancellationToken)
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
    public async Task Update(UpdateAgentDTO agent, CancellationToken cancellationToken)
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

        _mapper.Map(agent, currentAgent);
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


using companyApp.Server.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Interfaces;

public interface IAgentRepository
{
    Task<IEnumerable<AgentDTO>> Get(CancellationToken cancellationToken);
    Task<AgentDTO?> Get(int agentId, CancellationToken cancellationToken);
    void Create(AgentDTO agentDTO, CancellationToken cancellationToken);
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
    public async void Create(AgentDTO item, CancellationToken cancellationToken) // в разработке
    {
        //await context.AgentDTO.Add(item);
        context.SaveChanges();
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


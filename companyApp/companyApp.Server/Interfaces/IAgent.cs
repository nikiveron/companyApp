using companyApp.Server.Models.DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace companyApp.Server.Interfaces;

public interface IAgent
{
    Task<IEnumerable<AgentDTO>> Get(CancellationToken cancellationToken);
    Task<AgentDTO> Get(int agentId);
    void Create(AgentDTO agentDTO);
    void Update(AgentDTO agentDTO);
    Task<AgentDTO> Delete(int agentId);
}

public class Agent(ApplicationContext context) : IAgent
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
    public async Task<AgentDTO> Get(int Id) // в разработке
    {
        throw new NotImplementedException();    
    }
    public async void Create(AgentDTO item) // в разработке
    {
        //await context.AgentDTO.Add(item);
        context.SaveChanges();
    }
    public async void Update(AgentDTO updatedTodoItem) // в разработке
    {
        //AgentDTO currentItem = await Get(updatedTodoItem.Id);
        //currentItem.IsComplete = updatedTodoItem.IsComplete;
        //currentItem.TaskDescription = updatedTodoItem.TaskDescription;

        //await context.TodoItems.Update(currentItem);
        context.SaveChanges();
    }

    public async Task<AgentDTO> Delete(int Id) // в разработке
    {
        AgentDTO agentDTO = await Get(Id);

        if (agentDTO != null)
        {
            //await context.TodoItems.Remove(agentDTO);
            context.SaveChanges();
        }

        return agentDTO;
    }
}


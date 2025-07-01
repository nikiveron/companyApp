using companyApp.Server;
using companyApp.Server.DB_Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AgentController(ApplicationContext context, ILogger<CompanyController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgentEntity>>> GetAgents(CancellationToken cancellationToken)
    {
        try
        {
            var agents = await context.AgentsView
                .OrderBy(a =>  a.AgentId)
                .Take(10)
                .ToListAsync(cancellationToken);
            return Ok(agents);
        }
        catch (Exception ex) 
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }
}


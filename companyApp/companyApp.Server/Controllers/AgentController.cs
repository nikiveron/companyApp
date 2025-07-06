using companyApp.Server;
using companyApp.Server.Interfaces;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("agents")]
public class AgentController(IAgentRepository agent) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgentDTO>>> Get(CancellationToken cancellationToken)
    {
        try
        {
            var agents = await agent.Get(cancellationToken);
            return Ok(agents);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var a = await agent.Get(id, cancellationToken);

            if (a == null)
            {
                return NotFound();
            }

            return Ok(a);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }
}


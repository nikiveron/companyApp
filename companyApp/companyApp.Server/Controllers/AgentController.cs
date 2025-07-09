using companyApp.Server;
using companyApp.Server.Interfaces;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("agents")]
public class AgentController(IAgentRepository AgentRepository) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgentDTO>>> Get(CancellationToken cancellationToken)
    {
        try
        {
            var agents = await AgentRepository.Get(cancellationToken);
            return Ok(agents);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetAgent")]
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            var agent = await AgentRepository.Get(id, cancellationToken);

            if (agent == null)
            {
                return NotFound();
            }

            return Ok(agent);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAgentDTO agent, CancellationToken cancellationToken)
    {
        try
        {
            if (agent == null)
            {
                return BadRequest();
            }
            return Ok(await AgentRepository.Create(agent, cancellationToken));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
    {
        try
        {
            bool deletedStatus = await AgentRepository.Delete(id, cancellationToken);

            if (!deletedStatus)
            {
                return BadRequest("Агент не был удален.");
            }

            return Ok("Агент успешно удален.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] PutAgentDTO agent, CancellationToken cancellationToken)
    {
        try
        {
            if (agent == null || agent.Id != id)
            {
                return BadRequest();
            }

            var existingAgent = await AgentRepository.Get(id, cancellationToken);
            if (existingAgent == null)
            {
                return NotFound();
            }

            await AgentRepository.Update(agent, cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }

}


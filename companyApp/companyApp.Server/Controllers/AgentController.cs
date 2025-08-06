using companyApp.Server.Filters;
using companyApp.Server.Filters.Pagination;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Services.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("agents")]
public class AgentController(IMediator _mediator) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAgentDTO agent, CancellationToken cancellationToken)
    {
        try
        {
            if (agent == null)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(new CreateAgent.Query(agent), cancellationToken));
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

    [HttpGet]
    public async Task<ActionResult<List<ReadAgentDTO>>> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] InfoFilter infoFilter, CancellationToken cancellationToken)
    {
        try
        {
            GetAllAgents.Query query = new(Request.Path.Value, infoFilter, paginationFilter);
            var pagedResponse = await _mediator.Send(query, cancellationToken);
            return Ok(pagedResponse);
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
            var agent = await _mediator.Send(new GetAgent.Query(id), cancellationToken);

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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAgentDTO agent, CancellationToken cancellationToken)
    {
        try
        {
            if (agent == null || agent.Id != id)
            {
                return BadRequest();
            }

            var existingAgent = await _mediator.Send(new GetAgent.Query(id), cancellationToken);
            if (existingAgent == null)
            {
                return NotFound();
            }

            await _mediator.Send(new UpdateAgent.Query(agent), cancellationToken);
            return Ok();
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
            bool deletedStatus = await _mediator.Send(new DeleteAgent.Query(id), cancellationToken);

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
}


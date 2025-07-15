using AutoMapper;
using companyApp.Server.Filters;
using companyApp.Server.Filters.Pagination;
using companyApp.Server.Interfaces;
using companyApp.Server.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("agents")]
public class AgentController(IAgentRepository AgentRepository, IUriService uriService, ILogger<AgentController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAgentDTO agent, IMapper _mapper, CancellationToken cancellationToken)
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

    [HttpGet]
    public async Task<ActionResult<List<ReadAgentDTO>>> Get([FromQuery] PaginationFilter paginationFilter, [FromQuery] InfoFilter infoFilter, IMapper _mapper, CancellationToken cancellationToken)
    {
        try
        {
            var route = Request.Path.Value;
            var pageFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
            var informFilter = new InfoFilter(infoFilter.Inn, infoFilter.PhoneNumber, infoFilter.Email, infoFilter.OgrnFrom, infoFilter.OgrnTo, infoFilter.Priority);
            var getResult = await AgentRepository.Get(informFilter, pageFilter, cancellationToken);
            var pagedData = getResult.Item2;
            var totalRecords = getResult.Item1;
            var pagedResponse = PaginationHelper.CreatePagedReponse<ReadAgentDTO>(pagedData, pageFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}");
        }
    }

    [HttpGet("{id}", Name = "GetAgent")]
    public async Task<IActionResult> Get([FromRoute] int id, IMapper _mapper, CancellationToken cancellationToken)
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

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateAgentDTO agent, IMapper _mapper, CancellationToken cancellationToken)
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
}


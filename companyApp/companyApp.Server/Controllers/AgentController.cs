﻿using companyApp.Server;
using companyApp.Server.Interfaces;
using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("agents")]
public class AgentController(IAgent agent, ILogger<CompanyController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AgentDTO>>> GetAgents(CancellationToken cancellationToken)
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
}


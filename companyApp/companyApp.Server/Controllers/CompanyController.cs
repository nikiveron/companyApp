using companyApp.Server;
using companyApp.Server.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace companyApp.Server.Controllers;
[ApiController]
[Route("api/[controller]")]
public class CompanyController(ApplicationContext context, ILogger<CompanyController> logger) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompanyEntity>>> GetCompanies(CancellationToken cancellationToken)  // task - для асинхронности  
    {                                                                           // ActionResult - позволяет контролировать HTTP-ответы: статус код, Заголовки, Тело ответа, Тип контента
        try
        {
            var companies = await context.Companies
                .Where(c => c.DeletedAt == null) // Только неудаленные
                .Take(10)
                .ToListAsync(cancellationToken);
            return Ok(companies);   // Ok - одна из вариаций ActionResult, также мб BadRequest NotFound
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Внутренняя ошибка сервера: {ex.Message}"); //StatusCode - одна из вариаций ActionResul
        }
    }
}

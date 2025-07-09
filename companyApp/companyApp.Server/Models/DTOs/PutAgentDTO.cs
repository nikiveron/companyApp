using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace companyApp.Server.Models.DTOs;

public class PutAgentDTO
{
    [Required(ErrorMessage = "Идентификатор агента обязателен")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Краткое название компании обязательно")]
    public string ShortName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Полное название компании обязательно")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "ИНН обязателен")]
    public long Inn { get; set; }

    [Required(ErrorMessage = "КПП обязателен")]
    public int Kpp { get; set; }

    [Required(ErrorMessage = "ОГРН обязателен")]
    public long Ogrn { get; set; }

    [Required(ErrorMessage = "Дата выдачи ОГРН обязательна")]
    public DateTime OgrnDateOfIssue { get; set; }

    [Required(ErrorMessage = "Фамилия представителя обязательна")]
    public string RepLastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя представителя обязательно")]
    public string RepFirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Отчество представителя обязательно")]
    public string RepPatronymic { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email представителя обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string RepEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон представителя обязателен")]
    public string RepPhone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Список банков обязателен")]
    public List<int> Banks { get; set; } = [];

    [Required(ErrorMessage = "Признак важности агента обязателен")]
    public bool Priority { get; set; }

}

internal class PutAgentDTOCheck() : PutAgentDTO
{
    internal static async Task UniqueAgentCheck(ApplicationContext context, PutAgentDTO agent, CancellationToken cancellationToken)
    {
        if (await context.Agents.AnyAsync(c => c.Company.RepEmail == agent.RepEmail && c.AgentId != agent.Id, cancellationToken))
            throw new ArgumentException("Агент с таким представителем уже существует. Проверьте Email представителя.");
        if (await context.Agents.AnyAsync(c => c.Company.RepPhone == agent.RepPhone && c.AgentId != agent.Id, cancellationToken))
            throw new ArgumentException("Агент с таким представителем уже существует. Проверьте номер телефона представителя.");
        if (await context.Agents.AnyAsync(c => c.Company.Inn == agent.Inn && c.AgentId != agent.Id, cancellationToken))
            throw new ArgumentException("Агент с таким ИНН уже существует.");
        if (await context.Agents.AnyAsync(c => c.Company.Kpp == agent.Kpp && c.AgentId != agent.Id, cancellationToken))
            throw new ArgumentException("Агент с таким КПП уже существует.");
        if (await context.Agents.AnyAsync(c => c.Company.Ogrn == agent.Ogrn && c.AgentId != agent.Id, cancellationToken))
            throw new ArgumentException("Агент с таким ОГРН уже существует.");
    }
}

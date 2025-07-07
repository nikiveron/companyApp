using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApp.Server.Models.DTOs;

public class CreateAgentDTO()
{
    [Required(ErrorMessage = "Фамилия представителя обязательна")]
    public string RepLastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Имя представителя обязательно")]
    public string RepFirstName { get; set; } = string.Empty;

    public string RepPatronymic { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email представителя обязателен")]
    [EmailAddress(ErrorMessage = "Некорректный формат email")]
    public string RepEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Телефон представителя обязателен")]
    public string RepPhone { get; set; } = string.Empty;

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

    public List<int> Banks { get; set; } = [];
    public bool Priority { get; set; }
}

internal class CreateAgentDTOCheck() : CreateAgentDTO
{
    internal static async Task UniqueAgentCheck(ApplicationContext context, CreateAgentDTO agent, CancellationToken cancellationToken)
    {
        if (await context.Agents.AnyAsync(c => c.Company.RepEmail == agent.RepEmail, cancellationToken))
            throw new ArgumentException("Агент с таким представителем уже существует. Проверьте Email представителя.");
        if (await context.Agents.AnyAsync(c => c.Company.RepPhone == agent.RepPhone, cancellationToken))
            throw new ArgumentException("Агент с таким представителем уже существует. Проверьте номер телефона представителя.");
        if (await context.Agents.AnyAsync(c => c.Company.Inn == agent.Inn, cancellationToken))
            throw new ArgumentException("Агент с таким ИНН уже существует.");
        if (await context.Agents.AnyAsync(c => c.Company.Kpp == agent.Kpp, cancellationToken))
            throw new ArgumentException("Агент с таким КПП уже существует.");
        if (await context.Agents.AnyAsync(c => c.Company.Ogrn == agent.Ogrn, cancellationToken))
            throw new ArgumentException("Агент с таким ОГРН уже существует.");
    }
}

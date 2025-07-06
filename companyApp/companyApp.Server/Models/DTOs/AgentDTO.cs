using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApp.Server.Models.DTOs;

public class AgentDTO
{
    public int AgentId { get; set; }

    public string RepLastName { get; set; } = string.Empty;
    public string RepFirstName { get; set; } = string.Empty;
    public string RepPatronymic { get; set; } = string.Empty;
    public string RepEmail { get; set; } = string.Empty;
    public string RepPhone { get; set; } = string.Empty;

    public string ShortName { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
    public long Inn { get; set; }
    public int Kpp { get; set; }
    public long Ogrn { get; set; }
    public DateTime OgrnDateOfIssue { get; set; }

    public List<BankDTO> Banks { get; set; } = new List<BankDTO>();

    public bool Priority { get; set; }
}

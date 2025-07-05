using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApp.Server.Models.Entities;
[Table("bank")]
public class BankEntity
{
    [Key]
    [Column("bank_id")]
    public int BankId { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public List<AgentEntity> Agents { get; set; } = new List<AgentEntity>();
    // Навигационное свойство
    public CompanyEntity Company { get; set; } = null!;
}
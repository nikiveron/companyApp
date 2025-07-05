using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApp.Server.Models.Entities;
[Table("agent")]
public class AgentEntity
{
    [Key]
    [Column("agent_id")]
    public int AgentId { get; set; }  

    [Required]
    [Column("priority")]
    public bool Priority { get; set; }

    [Column("deleted_at")]
    public DateTime? DeletedAt { get; set; }

    public List<BankEntity> Banks { get; set; } = new List<BankEntity>();
    // Навигационное свойство
    public CompanyEntity Company { get; set; } = null!;
}

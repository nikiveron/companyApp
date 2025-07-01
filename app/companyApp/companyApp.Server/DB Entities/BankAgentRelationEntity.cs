using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApplication.Server.DB_Entities
{
    [Table("bank_agent_relation")]
    public class BankAgentRelationEntity
    {
        [Key]
        [Column("bank_id")]
        public int BankId { get; set; }
        
        [Key]
        [Column("agent_id")]
        public int AgentId { get; set; }
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        
        // Навигационные свойства
        public virtual BankEntity Bank { get; set; } = null!;
        public virtual AgentEntity Agent { get; set; } = null!;
    }
} 
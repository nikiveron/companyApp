using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApplication.Server.DB_Entities
{
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
        
        // Навигационное свойство
        public virtual CompanyEntity Company { get; set; } = null!;
    }
} 
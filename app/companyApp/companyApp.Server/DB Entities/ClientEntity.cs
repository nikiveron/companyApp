using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApplication.Server.DB_Entities
{
    [Table("client")]
    public class ClientEntity
    {
        [Key]
        [Column("client_id")]
        public int ClientId { get; set; }
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        
        // Навигационное свойство
        public virtual CompanyEntity Company { get; set; } = null!;
    }
} 
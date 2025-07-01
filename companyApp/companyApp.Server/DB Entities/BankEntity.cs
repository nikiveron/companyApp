using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApplication.Server.DB_Entities
{
    [Table("bank")]
    public class BankEntity
    {
        [Key]
        [Column("bank_id")]
        public int BankId { get; set; }
        
        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        
        // Навигационное свойство
        public virtual CompanyEntity Company { get; set; } = null!;
    }
} 
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApplication.Server.DB_Entities
{
    [Table("company")]
    public class CompanyEntity
    {
        [Key]
        [Column("company_id")]
        public int CompanyId { get; set; }
        
        [Required]
        [Column("short_name")]
        [StringLength(30)]
        public string ShortName { get; set; } = string.Empty;
        
        [Required]
        [Column("full_name")]
        [StringLength(30)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [Column("inn")]
        public long Inn { get; set; }
        
        [Required]
        [Column("kpp")]
        public int Kpp { get; set; }
        
        [Required]
        [Column("ogrn")]
        public long Ogrn { get; set; }
        
        [Required]
        [Column("ogrn_date_of_issue")]
        public DateTime OgrnDateOfIssue { get; set; }
        
        [Required]
        [Column("rep_last_name")]
        [StringLength(30)]
        public string RepLastName { get; set; } = string.Empty;
        
        [Required]
        [Column("rep_first_name")]
        [StringLength(30)]
        public string RepFirstName { get; set; } = string.Empty;
        
        [Required]
        [Column("rep_patronymic")]
        [StringLength(30)]
        public string RepPatronymic { get; set; } = string.Empty;
        
        [Required]
        [Column("rep_email")]
        [StringLength(30)]
        public string RepEmail { get; set; } = string.Empty;
        
        [Required]
        [Column("rep_phone")]
        [StringLength(30)]
        public string RepPhone { get; set; } = string.Empty;
        
        [Column("deleted_at")]
        public DateTimeOffset? DeletedAt { get; set; }
    }
}

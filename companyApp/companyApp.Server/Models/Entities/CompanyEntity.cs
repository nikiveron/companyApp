using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApp.Server.Models.Entities;
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
    [StringLength(12)]
    public string Inn { get; set; } = string.Empty;

    [Required]
    [Column("kpp")]
    [StringLength(9)]
    public string Kpp { get; set; } = string.Empty;

    [Required]
    [Column("ogrn")]
    [StringLength(13)]
    public string Ogrn { get; set; } = string.Empty;

    [Required]
    [Column("ogrn_date_of_issue")]
    public DateOnly OgrnDateOfIssue { get; set; }

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

    public AgentEntity? Agent { get; set; }
    public BankEntity? Bank { get; set; }
    public ClientEntity? Client { get; set; }
}


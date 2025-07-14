using AutoMapper;
using companyApp.Server.Models.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace companyApp.Server.Models.DTOs;

public class ReadAgentDTO
{
    public int AgentId { get; set; }

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
    public DateOnly OgrnDateOfIssue { get; set; }
    
    public List<BankDTO> Banks { get; set; } = [];
    public bool Priority { get; set; }
}
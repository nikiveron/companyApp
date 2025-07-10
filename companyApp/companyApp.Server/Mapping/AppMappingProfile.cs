using companyApp.Server.Models.DTOs;
using companyApp.Server.Models.Entities;
using AutoMapper;

namespace companyApp.Server.Mapping;
public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<AgentEntity, ReadAgentDTO>()  // AgentEntity - источник, ReadAgentDTO - приемник
            .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.Company.ShortName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Company.FullName))
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Company.Inn))
            .ForMember(dest => dest.Kpp, opt => opt.MapFrom(src => src.Company.Kpp))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Company.Ogrn))
            .ForMember(dest => dest.OgrnDateOfIssue, opt => opt.MapFrom(src => src.Company.OgrnDateOfIssue))
            .ForMember(dest => dest.RepLastName, opt => opt.MapFrom(src => src.Company.RepLastName))
            .ForMember(dest => dest.RepFirstName, opt => opt.MapFrom(src => src.Company.RepFirstName))
            .ForMember(dest => dest.RepPatronymic, opt => opt.MapFrom(src => src.Company.RepPatronymic))
            .ForMember(dest => dest.RepEmail, opt => opt.MapFrom(src => src.Company.RepEmail))
            .ForMember(dest => dest.RepPhone, opt => opt.MapFrom(src => src.Company.RepPhone))
            .ForMember(dest => dest.Banks, opt => opt.MapFrom(src => src.Banks));

        CreateMap<BankEntity, BankDTO>()
            .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.Company.ShortName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.Company.FullName))
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Company.Inn))
            .ForMember(dest => dest.Kpp, opt => opt.MapFrom(src => src.Company.Kpp))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Company.Ogrn))
            .ForMember(dest => dest.OgrnDateOfIssue, opt => opt.MapFrom(src => src.Company.OgrnDateOfIssue))
            .ForMember(dest => dest.RepLastName, opt => opt.MapFrom(src => src.Company.RepLastName))
            .ForMember(dest => dest.RepFirstName, opt => opt.MapFrom(src => src.Company.RepFirstName))
            .ForMember(dest => dest.RepPatronymic, opt => opt.MapFrom(src => src.Company.RepPatronymic))
            .ForMember(dest => dest.RepEmail, opt => opt.MapFrom(src => src.Company.RepEmail))
            .ForMember(dest => dest.RepPhone, opt => opt.MapFrom(src => src.Company.RepPhone));
            
        CreateMap<CreateAgentDTO, AgentEntity>()
            .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => (DateTime?)null))
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Company, opt => opt.Ignore())
            .ForMember(dest => dest.Banks, opt => opt.Ignore());

        CreateMap<CreateAgentDTO, CompanyEntity>()
            .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.ShortName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Inn))
            .ForMember(dest => dest.Kpp, opt => opt.MapFrom(src => src.Kpp))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Ogrn))
            .ForMember(dest => dest.OgrnDateOfIssue, opt => opt.MapFrom(src => src.OgrnDateOfIssue))
            .ForMember(dest => dest.RepLastName, opt => opt.MapFrom(src => src.RepLastName))
            .ForMember(dest => dest.RepFirstName, opt => opt.MapFrom(src => src.RepFirstName))
            .ForMember(dest => dest.RepPatronymic, opt => opt.MapFrom(src => src.RepPatronymic))
            .ForMember(dest => dest.RepEmail, opt => opt.MapFrom(src => src.RepEmail))
            .ForMember(dest => dest.RepPhone, opt => opt.MapFrom(src => src.RepPhone))
            .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => (DateTime?)null));

        CreateMap<UpdateAgentDTO, AgentEntity>()
            .ForMember(dest => dest.Priority, opt => opt.MapFrom(src => src.Priority))
            .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src))
            .ForMember(dest => dest.Banks, opt => opt.Ignore());

        CreateMap<UpdateAgentDTO, CompanyEntity>()
            .ForMember(dest => dest.ShortName, opt => opt.MapFrom(src => src.ShortName))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Inn, opt => opt.MapFrom(src => src.Inn))
            .ForMember(dest => dest.Kpp, opt => opt.MapFrom(src => src.Kpp))
            .ForMember(dest => dest.Ogrn, opt => opt.MapFrom(src => src.Ogrn))
            .ForMember(dest => dest.OgrnDateOfIssue, opt => opt.MapFrom(src => src.OgrnDateOfIssue))
            .ForMember(dest => dest.RepLastName, opt => opt.MapFrom(src => src.RepLastName))
            .ForMember(dest => dest.RepFirstName, opt => opt.MapFrom(src => src.RepFirstName))
            .ForMember(dest => dest.RepPatronymic, opt => opt.MapFrom(src => src.RepPatronymic))
            .ForMember(dest => dest.RepEmail, opt => opt.MapFrom(src => src.RepEmail))
            .ForMember(dest => dest.RepPhone, opt => opt.MapFrom(src => src.RepPhone)); 

    }
}

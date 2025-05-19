using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Service.Commands;
using Yenilen.Application.Features.Service.Queries;
using Yenilen.Domain.Common.Enums;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class ServiceMappingProfile:Profile
{
    public ServiceMappingProfile()
    {
        CreateMap<Service, ServiceDto>()
            .ForMember(s => s.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(s => s.ServiceId, opt => opt.MapFrom(src => src.Id))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(s => s.Duration, opt => opt.MapFrom(src => src.Duration));
        
        CreateMap<Service, GetServicesByCategoryIdQueryResponse>()
            .ForMember(s => s.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(s => s.ServiceId, opt => opt.MapFrom(src => src.Id))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(s => s.Duration, opt => opt.MapFrom(src => src.Duration));


        CreateMap<CreateServiceCommand, Service>()
            .ForMember(s => s.CategoryId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.Description, opt => opt.MapFrom(src => src.Description))
            .ForMember(s => s.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(s => s.Currency, opt =>
                opt.MapFrom(src => Enum.Parse<CurrencyType>(src.CurrencyType!)))
            .ForMember(s => s.Duration, opt => opt.MapFrom(src => src.Duration));

    }
}
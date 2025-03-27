using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class ServiceMappingProfile:Profile
{
    public ServiceMappingProfile()
    {
        CreateMap<Service, ServiceDto>()
            .ForMember(s => s.CatalogId, opt => opt.MapFrom(src => src.CategoryId))
            .ForMember(s => s.ServiceId, opt => opt.MapFrom(src => src.Id))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.Price, opt => opt.MapFrom(src => src.Price))
            .ForMember(s => s.Duration, opt => opt.MapFrom(src => src.Duration));

    }
}
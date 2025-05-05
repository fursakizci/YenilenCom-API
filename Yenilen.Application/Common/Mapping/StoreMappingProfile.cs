using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class StoreMappingProfile:Profile
{
    public StoreMappingProfile()
    {
        CreateMap<Store, StoreIndividualDto>()
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.StoreName))
            .ForMember(s => s.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(s => s.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(s => s.StoreWorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
            .ForMember(s => s.Reviews, opt => opt.MapFrom(src => src.Reviews))
            .ForMember(s => s.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(s => s.StaffMembers, opt => opt.MapFrom(src => src.StaffMembers))
            .ForMember(s => s.About, opt => opt.MapFrom(src => src.About));

    }
    
}
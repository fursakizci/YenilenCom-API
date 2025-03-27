using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class StaffMappingProfile:Profile
{
    public StaffMappingProfile()
    {
        CreateMap<Staff, StaffDto>()
            .ForMember(s => s.StaffId, opt => opt.MapFrom(src => src.Id))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.ImageUrl, opt => opt.MapFrom(src => src.Image.ImageUrl));
    }
}
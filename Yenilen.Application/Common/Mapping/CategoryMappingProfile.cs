using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class CategoryMappingProfile:Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name));
    }
}
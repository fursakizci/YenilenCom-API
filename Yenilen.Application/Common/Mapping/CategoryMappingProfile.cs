using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Category.Commands;
using Yenilen.Application.Features.Category.Queries;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class CategoryMappingProfile:Profile
{
    public CategoryMappingProfile()
    {
        CreateMap<Category, CategoryDto>()
            .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name));

        CreateMap<Category, GetCategoriesByStoreIdQueryResponse>()
            .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name));

        
        CreateMap<CreateCategoryCommand, Category>()
            .ForMember(c => c.StoreId, opt => opt.MapFrom(src => src.StoreId))
            .ForMember(c => c.Name, opt => opt.MapFrom(src => src.Name));
    }
}
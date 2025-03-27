using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class TagMappingProfile:Profile
{
    public TagMappingProfile()
    {
        CreateMap<Tag, TagDto>()
            .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name));
    }
}
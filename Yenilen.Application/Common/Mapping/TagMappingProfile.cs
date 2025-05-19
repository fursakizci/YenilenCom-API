using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Tag.Commands;
using Yenilen.Application.Features.Tag.Queries;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class TagMappingProfile:Profile
{
    public TagMappingProfile()
    {
        CreateMap<CreateTagCommand, Tag>()
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(t => t.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<Tag, TagDto>()
            .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(t => t.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));
        
        CreateMap<Tag, GetAllTagsQueryResponse>()
            .ForMember(t => t.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(t => t.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(t => t.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

    }
}
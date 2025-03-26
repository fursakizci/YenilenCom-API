using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class TagMappingProfile:Profile
{
    public TagMappingProfile()
    {
        CreateMap<Tag, TagDto>();
    }
}
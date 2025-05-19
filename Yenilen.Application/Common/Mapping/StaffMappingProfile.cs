using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.StaffMember.Commands;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class StaffMappingProfile:Profile
{
    public StaffMappingProfile()
    {
        CreateMap<Staff, StaffDto>()
            .ForMember(s => s.StoreId, opt => opt.MapFrom(src => src.Id))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.ImageUrl, opt => opt.MapFrom(src => src.Image.ImageUrl));
        
        CreateMap<Staff, GetStaffMembersByStoryIdQueryResponse>()
            .ForMember(s => s.StoreId, opt => opt.MapFrom(src => src.Id))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.ImageUrl, opt => opt.MapFrom(src => src.Image.ImageUrl));

        
        CreateMap<CreateStaffMemberCommand, Staff>()
            .ForMember(s => s.StoreId, opt => opt.MapFrom(src => src.StoreId))
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(s => s.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(s => s.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .ForMember(s => s.Bio, opt => opt.MapFrom(src => src.Bio))
            .ForPath(dest => dest.Image.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl));

    }
}
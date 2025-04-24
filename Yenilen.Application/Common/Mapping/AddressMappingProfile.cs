using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Users.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class AddressMappingProfile : Profile
{
    public AddressMappingProfile()
    {
        CreateMap<AddUserAddressCommand, Address>()
            .ForMember(a => a.Id, opt => opt.MapFrom(src => src.UserId))
            .ForMember(a => a.Latitude, opt => opt.MapFrom(src => src.Latitude))
            .ForMember(a => a.Longitude, opt => opt.MapFrom(src => src.Longitude))
            .ForMember(a => a.FullAddress, opt => opt.MapFrom(src => src.FullAddress))
            .ForMember(a => a.City, opt => opt.MapFrom(src => src.City))
            //.ForMember(a => a.Country, opt => opt.MapFrom(src => src.Country))
            .ForMember(a => a.District, opt => opt.MapFrom(src => src.District))
            .ForMember(a => a.Label, opt => opt.MapFrom(src => src.Label))
            //.ForMember(a => a.Region, opt => opt.MapFrom(src => src.Region))
            .ForMember(a => a.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
            .ForMember(a => a.PostCode, opt => opt.MapFrom(src => src.PostCode))
            .ForMember(a => a.Label, opt => opt.MapFrom(src => src.Label));

    }       
}
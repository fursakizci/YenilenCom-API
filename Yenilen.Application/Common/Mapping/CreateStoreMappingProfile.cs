using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class CreateStoreMappingProfile:Profile
{
    public CreateStoreMappingProfile()
    {
        CreateMap<CreateStoreCommand, Store>()
            .ForMember(a => a.StoreName, opt => opt.MapFrom(src => src.Name))
            .ForMember(a => a.ManagerName, opt => opt.MapFrom(src => src.ManagerName))
            .ForMember(a => a.ManagerPhone, opt => opt.MapFrom(src => src.ManagerPhone))
            .ForMember(a => a.WorkingHours, opt => opt.MapFrom(src => src.StoreWorkingHours));
        
        CreateMap<StoreWorkingHourDto, StoreWorkingHour>()
            .ForMember(s => s.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
            .ForMember(s => s.OpeningTime, opt => opt.MapFrom(src => src.OpeningTime))
            .ForMember(s => s.ClosingTime, opt => opt.MapFrom(src => src.ClosingTime))
            .ForMember(s => s.IsClosed, opt => opt.MapFrom(src => src.IsClosed));
        
        CreateMap<AddressDto, Address>()
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
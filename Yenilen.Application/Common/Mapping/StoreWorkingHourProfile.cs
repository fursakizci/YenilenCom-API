using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Store.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class StoreWorkingHourProfile:Profile
{
    public StoreWorkingHourProfile()
    {
        CreateMap<Store, StoreDetailsDto>()
            .ForMember(s => s.Name, opt => opt.MapFrom(src => src.StoreName))
            .ForMember(s => s.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(s => s.Images, opt => opt.MapFrom(src => src.Images))
            .ForMember(s => s.StoreWorkingHours, opt => opt.MapFrom(src => src.WorkingHours))
            .ForMember(s => s.Reviews, opt => opt.MapFrom(src => src.Reviews))
            .ForMember(s => s.Categories, opt => opt.MapFrom(src => src.Categories))
            .ForMember(s => s.StaffMembers, opt => opt.MapFrom(src => src.StaffMembers))
            .ForMember(s => s.About, opt => opt.MapFrom(src => src.About));

        // CreateMap<Address, AddressDto>()
        //     .ForMember(a => a.Latitude, opt => opt.MapFrom(src => src.Latitude))
        //     .ForMember(a => a.Longitude, opt => opt.MapFrom(src => src.Longitude))
        //     .ForMember(a => a.FullAddress, opt => opt.MapFrom(src => src.FullAddress))
        //     .ForMember(a => a.City, opt => opt.MapFrom(src => src.City))
        //     .ForMember(a => a.District, opt => opt.MapFrom(src => src.District))
        //     .ForMember(a => a.Label, opt => opt.MapFrom(src => src.Label))
        //     .ForMember(a => a.CountryCode, opt => opt.MapFrom(src => src.CountryCode))
        //     .ForMember(a => a.PostCode, opt => opt.MapFrom(src => src.PostCode))
        //     .ForMember(a => a.Label, opt => opt.MapFrom(src => src.Label));

        // CreateMap<Image, ImageDto>()
        //     .ForMember(i => i.Url, opt => opt.MapFrom(src => src.ImageUrl));

        CreateMap<StoreWorkingHour, StoreWorkingHourDto>()
            .ForMember(s => s.ClosingTime, opt => opt.MapFrom(src => src.ClosingTime))
            .ForMember(s => s.DayOfWeek, opt => opt.MapFrom(src => src.DayOfWeek))
            .ForMember(s => s.OpeningTime, opt => opt.MapFrom(src => src.OpeningTime))
            .ForMember(s => s.IsClosed, opt => opt.MapFrom(src => src.IsClosed));

        // CreateMap<Service, ServiceDto>()
        //     .ForMember(s => s.Name, opt => opt.MapFrom(src => src.Name));

    }
    
}
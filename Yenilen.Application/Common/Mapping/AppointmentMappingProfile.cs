using AutoMapper;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Booking.Commands;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Common.Mapping;

public class AppointmentMappingProfile:Profile
{
    public AppointmentMappingProfile()
    {
        CreateMap<CreateAppointmentCommand, Appointment>()
            .ForMember(a => a.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(a => a.StoreId, opt => opt.MapFrom(src => src.StoreId))
            .ForMember(a => a.StaffId, opt => opt.MapFrom(src => src.StaffId))
            .ForMember(a => a.StartTime, opt => opt.MapFrom(src => src.AppointmentDate))
            .ForMember(a => a.Duration, opt => opt.MapFrom(src => src.ServiceDuration))
            .ForMember(a => a.Status, opt => opt.MapFrom(src => src.AppointmentStatus))
            .ForMember(a => a.Note, opt => opt.MapFrom(src => src.Note));
    }
}
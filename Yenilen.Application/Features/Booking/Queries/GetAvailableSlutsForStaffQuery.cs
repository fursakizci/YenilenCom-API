using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetAvailableSlutsForStaffQuery:IRequest<List<AvailableDateDto>>
{
    public int StaffId { get; set; } = 1;
    public int TotalServiceDuration { get; set; }
}
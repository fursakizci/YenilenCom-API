using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetAvailableSlotsQuery:IRequest<List<AvailableDateDto>>
{
    public int StoreId { get; set; }
    public DateTime StartingDate { get; set; }
    public int TotalServiceDuration { get; set; }
    public string TimeZoneId { get; set; }
}
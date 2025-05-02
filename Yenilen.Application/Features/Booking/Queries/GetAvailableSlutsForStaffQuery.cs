using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetAvailableSlutsForStaffQuery:IRequest<List<AvailableDateDto>>
{
    public int StaffId { get; set; }
    public DateTime CurrentTime { get; set; }
    public int UserId { get; set; }
    public int StoreId { get; set; }
    public List<int> Services { get; set; }
}
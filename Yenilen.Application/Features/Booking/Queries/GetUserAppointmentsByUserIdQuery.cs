using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetUserAppointmentsByUserIdQuery:IRequest<IQueryable<AppointmentDto>>
{
    public int UserId { get; set; }
}
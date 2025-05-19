using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.Booking.Queries;

public sealed class GetUserAppointmentsByUserIdQuery:IRequest<Result<IQueryable<GetUserAppointmentsByUserIdQueryResponse>>>
{
    public int UserId { get; set; }
}



public sealed class GetUserAppointmentsByUserIdQueryResponse
{
    public int? UserId { get; set; }
    //public List<ServiceDto> Services { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public string Note { get; set; }
}
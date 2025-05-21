using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.StaffMember.Queries;

public sealed class GetStaffAppointmentsByStaffIdQuery:IRequest<Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>>
{
    public int StaffId { get; set; }
}

public sealed class GetStaffAppointmentsByStaffIdQueryResponse
{
    public int? UserId { get; set; }
    //public List<ServiceDto> Services { get; set; }
    public DateTime StartTime { get; set; }
    public int Duration { get; set; }
    public string Note { get; set; }
}
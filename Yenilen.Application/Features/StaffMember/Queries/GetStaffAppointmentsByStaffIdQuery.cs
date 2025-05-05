using MediatR;
using Yenilen.Application.DTOs;

namespace Yenilen.Application.Features.StaffMember.Queries;

public sealed class GetStaffAppointmentsByStaffIdQuery:IRequest<List<AppointmentDto>>
{
    public int StaffId { get; set; }
}
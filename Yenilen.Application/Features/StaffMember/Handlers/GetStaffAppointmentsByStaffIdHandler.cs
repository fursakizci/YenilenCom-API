using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Application.Features.StaffMember.Handlers;

internal sealed class GetStaffAppointmentsByStaffIdHandler:IRequestHandler<GetStaffAppointmentsByStaffIdQuery,List<AppointmentDto>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public GetStaffAppointmentsByStaffIdHandler(IStaffRepository staffRepository, IAppointmentRepository appointmentRepository)
    {
        _staffRepository = staffRepository;
        _appointmentRepository = appointmentRepository;
    }
    
    public async Task<List<AppointmentDto>> Handle(GetStaffAppointmentsByStaffIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _staffRepository.GetByIdAsync(request.StaffId);
    
        if (user == null)
        {
            throw new InvalidOperationException("Girdiginiz calisana ait bilgilere erisilemiyor");
        }
    
        var appointments =
            _appointmentRepository.Where(a => a.StaffId == request.StaffId);
        
        throw new NotImplementedException();
    }
}
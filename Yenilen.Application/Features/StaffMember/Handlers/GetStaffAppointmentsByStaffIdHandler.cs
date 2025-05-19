using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Application.Features.StaffMember.Handlers;

internal sealed class GetStaffAppointmentsByStaffIdHandler:IRequestHandler<GetStaffAppointmentsByStaffIdQuery,Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IAppointmentRepository _appointmentRepository;

    public GetStaffAppointmentsByStaffIdHandler(IStaffRepository staffRepository, IAppointmentRepository appointmentRepository)
    {
        _staffRepository = staffRepository;
        _appointmentRepository = appointmentRepository;
    }
    
    public async Task<Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>> Handle(GetStaffAppointmentsByStaffIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _staffRepository.GetByIdAsync(request.StaffId);
    
        if (user == null)
        {
            throw new InvalidOperationException("Girdiginiz calisana ait bilgilere erisilemiyor");
        }
    
        var appointments =
            await _appointmentRepository.Where(a => a.StaffId == request.StaffId).ToListAsync();

        var result = new List<GetStaffAppointmentsByStaffIdQueryResponse>();
        
        foreach (var appointment in appointments)
        {
            var appointmentDto = new GetStaffAppointmentsByStaffIdQueryResponse
            {
                UserId =  appointment.StaffId,
                StartTime = appointment.StartTime,
                Duration = (int)appointment.Duration.TotalMinutes
            };
            result.Add(appointmentDto);
        }

        return Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>.Succeed(result);
    }
}
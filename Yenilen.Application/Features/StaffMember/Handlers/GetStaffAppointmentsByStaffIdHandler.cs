using MediatR;
using Microsoft.EntityFrameworkCore;
using TS.Result;
using Yenilen.Application.Features.StaffMember.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Application.Services.Common;

namespace Yenilen.Application.Features.StaffMember.Handlers;

internal sealed class GetStaffAppointmentsByStaffIdHandler:IRequestHandler<GetStaffAppointmentsByStaffIdQuery,Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IStoreOwnerPolicyService _storeOwnerPolicyService;
    private readonly IAppointmentRepository _appointmentRepository;

    public GetStaffAppointmentsByStaffIdHandler(IStaffRepository staffRepository,
        IStoreOwnerPolicyService storeOwnerPolicyService,
        IAppointmentRepository appointmentRepository)
    {
        _staffRepository = staffRepository;
        _storeOwnerPolicyService = storeOwnerPolicyService;
        _appointmentRepository = appointmentRepository;
    }
    
    public async Task<Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>> Handle(GetStaffAppointmentsByStaffIdQuery request, CancellationToken cancellationToken)
    {
        //TODO: Buraya admin calendar entegrasyonunu tamamladiktan sonra tekrar bak.
        
        
        
        var appUserId = await _storeOwnerPolicyService.ValidateAndGetAppUserIdAsync(cancellationToken);

        if (!appUserId.IsSuccessful)
        {
            return Result<List<GetStaffAppointmentsByStaffIdQueryResponse>>.Failure(appUserId.StatusCode, appUserId.ErrorMessages ?? new List<string> { "Kullanıcı ID alınamadı." });
        }
        
    
        if (appUserId == null)
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
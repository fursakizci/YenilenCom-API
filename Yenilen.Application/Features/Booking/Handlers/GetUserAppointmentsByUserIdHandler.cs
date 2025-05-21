using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Booking.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Booking.Handlers;

internal sealed class GetUserAppointmentsByUserIdHandler:IRequestHandler<GetUserAppointmentsByUserIdQuery,Result<IQueryable<GetUserAppointmentsByUserIdQueryResponse>>>
{
    private readonly IAppointmentRepository _appointmentRepository;

    public GetUserAppointmentsByUserIdHandler(IAppointmentRepository appointmentRepository)
    {
        _appointmentRepository = appointmentRepository;
    }
    
    public Task<Result<IQueryable<GetUserAppointmentsByUserIdQueryResponse>>> Handle(GetUserAppointmentsByUserIdQuery request, CancellationToken cancellationToken)
    {
        var userAppointments = _appointmentRepository.Where(a => a.CustomerId == request.UserId)
            .Select(a => new GetUserAppointmentsByUserIdQueryResponse
            {
                UserId = a.CustomerId,
                StartTime = a.StartTime,
                Duration = (int)a.Duration.TotalMinutes,
                Note = a.Note ?? string.Empty
            });
        
        return Task.FromResult(Result<IQueryable<GetUserAppointmentsByUserIdQueryResponse>>.Succeed(userAppointments));
    }
}
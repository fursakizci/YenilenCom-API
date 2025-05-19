using MediatR;
using TS.Result;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Booking.Queries;
using Yenilen.Application.Interfaces;

namespace Yenilen.Application.Features.Booking.Handlers;

internal sealed class GetAvailableSlutsForStaffHandler:IRequestHandler<GetAvailableSlutsForStaffQuery,Result<List<GetAvailableSlutsForStaffQueryResponse>>>
{
    private readonly IStaffRepository _staffRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IStaffWorkingHourRepository _staffWorkingHourRepository;
    
    public GetAvailableSlutsForStaffHandler(IStaffRepository staffRepository, IAppointmentRepository appointmentRepository, IStaffWorkingHourRepository staffWorkingHourRepository)
    {
        _staffRepository = staffRepository;
        _appointmentRepository = appointmentRepository;
        _staffWorkingHourRepository = staffWorkingHourRepository;

    }
    
    public async Task<Result<List<GetAvailableSlutsForStaffQueryResponse>>> Handle(GetAvailableSlutsForStaffQuery request, CancellationToken cancellationToken)
    {
        var staff = await _staffRepository.GetByIdAsync(request.StaffId);  
        
        if(staff == null){
            throw new InvalidOperationException("Verilen bilgilere ait calisan bulunamadi.");
        }

        var today = DateTime.UtcNow.Date;
        var result = new List<GetAvailableSlutsForStaffQueryResponse>();

        var staffWorkingHours = await _staffWorkingHourRepository.GetStaffWorkingHoursByStaffId(staff.Id);

        for (int i = 0; i < 14; i++)
        {
            var date = today.AddDays(i);
            var workingHour = staffWorkingHours.FirstOrDefault(w => w.DayOfWeek == date.DayOfWeek);
            
            if(workingHour == null) continue;

            var appointments = await 
                _appointmentRepository.GetAppointmentsByStaffAndDateAsync(request.StaffId, date.Date);

            var appointmentSlots =  appointments
                .Select(a => new 
                { 
                    Start = a.StartTime.TimeOfDay, 
                    End = a.StartTime.TimeOfDay + a.Duration 
                })
                .ToList();

            var timeSlots = new List<TimeSlotDto>();
            var slotStart = workingHour.StartTime;
            var slotEnd = slotStart.Add(TimeSpan.FromMinutes(request.TotalServiceDuration));

            while (slotEnd <= workingHour.EndTime)
            {
                bool isOverlapping = appointmentSlots.Any(a =>
                    (slotStart < a.End && slotEnd > a.Start));

                if (!isOverlapping)
                {
                    timeSlots.Add(new TimeSlotDto()
                    {
                        StartTimeInSeconds = (int)slotStart.TotalSeconds,
                        FormattedTime = date.Date.Add(slotStart).ToString("h:mm tt"),
                        Duration = request.TotalServiceDuration
                    });
                }

                slotStart = slotStart.Add(TimeSpan.FromMinutes(15));
                slotEnd = slotStart.Add(TimeSpan.FromMinutes(request.TotalServiceDuration));
            }

            if (timeSlots.Any())
            {
                result.Add(new GetAvailableSlutsForStaffQueryResponse
                {
                    Date = date,
                    DayName = date.ToString("dddd"),
                    Year = date.Year.ToString(),
                    Month = date.ToString("MMMM"),
                    DayOfMonth = date.Day.ToString(),
                    TimeSlots = timeSlots
                });
            }
        }
        return Result<List<GetAvailableSlutsForStaffQueryResponse>>.Succeed(result);
    }
}
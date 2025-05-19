using MediatR;
using Yenilen.Application.DTOs;
using Yenilen.Application.Features.Booking.Queries;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.DTOs;
using Yenilen.Domain.Entities;

namespace Yenilen.Application.Features.Booking.Handlers;

internal sealed class GetAvailableSlotsHandler:IRequestHandler<GetAvailableSlotsQuery,List<AvailableDateDto>>
{
    
    private readonly IStaffRepository _staffRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IStoreRepository _storeRepository;
    private readonly IStaffWorkingHourRepository _staffWorkingHourRepository;
    private const int SlotDuration = 15;

    public GetAvailableSlotsHandler(IStaffRepository staffRepository, IStoreRepository storeRepository, IAppointmentRepository appointmentRepository, IStaffWorkingHourRepository staffWorkingHourRepository)
    {
        _staffRepository = staffRepository;
        _appointmentRepository = appointmentRepository;
        _storeRepository = storeRepository;
        _staffWorkingHourRepository = staffWorkingHourRepository;        
    }
    
   public async Task<List<AvailableDateDto>> Handle(GetAvailableSlotsQuery request,
    CancellationToken cancellationToken)
{
    var timeZone = TimeZoneInfo.FindSystemTimeZoneById(request.TimeZoneId);
    
    var result = new List<AvailableDateDto>();
    
    var startDate = TimeZoneInfo.ConvertTimeFromUtc(request.StartingDate.Date, timeZone).Date;
    
    var staffList = await _staffRepository.GetStaffMembersByStoreIdAsync(request.StoreId) ;
    
    var staffWithTotalServiceDuration = new List<DailyServiceDurationDto>();
   
    foreach (var staff in staffList)
    {
        var dailyTotalServiceDuration = await _appointmentRepository
            .GetDailyServiceDurationsByStaffIdAsync(staff.Id, request.StartingDate.Date, request.StartingDate.AddDays(14).Date, request.TimeZoneId);
        
        staffWithTotalServiceDuration.AddRange(dailyTotalServiceDuration); 
    }

    var store = await _storeRepository.GetStoreWithWorkingTimesAsync(request.StoreId);
    
    foreach (var dateOffset in Enumerable.Range(0, 14))
    {
        var currentDate = startDate.AddDays(dateOffset); // Bu, yerel saat diliminde günün başlangıcıdır.

        var storeWorkingHoursOnCurrentDay = store.WorkingHours.FirstOrDefault(sw => sw.DayOfWeek == currentDate.DayOfWeek);

        if (storeWorkingHoursOnCurrentDay == null || storeWorkingHoursOnCurrentDay.IsClosed)
        {
           continue; 
        }
        
        var storeOpeningTime = storeWorkingHoursOnCurrentDay.OpeningTime;
        var storeClosingTime = storeWorkingHoursOnCurrentDay.ClosingTime;
            
        var dailySlots = new AvailableDateDto()
        {
            Date = currentDate,
            DayName = currentDate.DayOfWeek.ToString(),
            Year = currentDate.Year.ToString(),
            Month = currentDate.ToString("MMMM", System.Globalization.CultureInfo.InvariantCulture), // Ay ismi için CultureInfo belirtebilirsiniz
            DayOfMonth = currentDate.Day.ToString(),
            TimeSlots = new List<TimeSlotDto>()
        };
        
        var possibleSlotStartOffsets = Enumerable.Range(0, 
                (int)Math.Max(0, (storeClosingTime - storeOpeningTime).TotalMinutes / SlotDuration)) // Negatif süre olmaması için Math.Max
            .Select(i => storeOpeningTime.Add(TimeSpan.FromMinutes(i * SlotDuration)))
            .ToList();
        
        int requiredConsecutiveSlots = (int)Math.Ceiling((double)request.TotalServiceDuration / SlotDuration);
        
        var staffSortedByLoadForDate = staffWithTotalServiceDuration
            .Where(x => {
                // x.Date (UTC) yerel saate çevrildiğinde currentDate (yerel) ile aynı güne denk gelmeli
                DateTime staffDutyDateLocal = TimeZoneInfo.ConvertTimeFromUtc(x.Date, timeZone);
                return staffDutyDateLocal.Date == currentDate.Date;
            })
            .OrderBy(x => x.TotalDurationMinutes)
            .ToList();
        
        foreach (var staff in staffList)
        {
            if (staffWithTotalServiceDuration.Where(a=>a.Date == currentDate).Select(s => s.StaffId).Contains(staff.Id))
            {
                continue;
            }

            var staffDailyService = new DailyServiceDurationDto()
            {
                StaffId = staff.Id,
                TotalDurationMinutes = 0,
                Date = currentDate.Date,
                Appointments = new List<TimeRange>()
            };
            
            staffSortedByLoadForDate.Insert(0, staffDailyService);
        }

        for (int i = 0; i <= possibleSlotStartOffsets.Count - requiredConsecutiveSlots; i++)
        {
            var potentialAppointmentStartOffset = possibleSlotStartOffsets[i]; // Örn: 09:00:00 (TimeSpan)
            var potentialAppointmentEndOffset = possibleSlotStartOffsets[i + requiredConsecutiveSlots - 1] + TimeSpan.FromMinutes(SlotDuration); // Örn: 09:30:00 (TimeSpan)
            var proposedTimeWindow = new TimeRange(potentialAppointmentStartOffset, potentialAppointmentEndOffset);

            string assignedStaffIdForSlot = null;
            
            foreach (var staffInfo in staffSortedByLoadForDate)
            {
                // staffInfo.Appointments'ın, personelin o günkü meşgul olduğu TimeSpan aralıklarını
                // (TimeRange listesi olarak) içerdiği varsayılır.
                // Bu TimeRange'lerin yerel günün zaman dilimine göre olduğu kabul edilir.
                List<TimeRange> staffBusyTimeSpans = staffInfo.Appointments; 

                bool overlaps = staffBusyTimeSpans.Any(existingBusySpan => existingBusySpan.Overlaps(proposedTimeWindow));

                if (!overlaps)
                {
                    // Bu personel bu zaman aralığı için uygun.
                    // 'staffSortedByLoadForDate' en az yoğuna göre sıralı olduğu için, bu personel en iyi adaydır.
                    assignedStaffIdForSlot = staffInfo.StaffId.ToString();
                    break; // Bu zaman aralığı bu personele atandı, diğer personelleri kontrol etmeye gerek yok.
                }
            }

            if (assignedStaffIdForSlot != null)
            {
                dailySlots.TimeSlots.Add(new TimeSlotDto
                {
                    StartTimeInSeconds = (int)potentialAppointmentStartOffset.TotalSeconds,
                    FormattedTime = TimeOnly.FromTimeSpan(potentialAppointmentStartOffset).ToString("hh:mm tt"),
                    Duration = request.TotalServiceDuration, 
                    StaffId = Convert.ToInt32(assignedStaffIdForSlot)
                });
            }
        }
        
        if(dailySlots.TimeSlots.Any()){
             result.Add(dailySlots);
        }
    }

    return result;
 }
}


        


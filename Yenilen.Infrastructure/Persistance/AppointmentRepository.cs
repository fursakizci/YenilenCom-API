using Microsoft.EntityFrameworkCore;
using Yenilen.Application.Interfaces;
using Yenilen.Domain.DTOs;
using Yenilen.Domain.Entities;
using Yenilen.Infrastructure.DataAccess;

namespace Yenilen.Infrastructure.Persistance;

internal sealed class AppointmentRepository:GenericRepository<Appointment,AppDbContext>,IAppointmentRepository
{ 
    private readonly AppDbContext _context;
    private readonly DbSet<Appointment> _dbSet;
    public AppointmentRepository(AppDbContext context) : base(context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<Appointment>();
    }

    public async Task<List<Appointment>> GetAppointmentsByStaffAndDateAsync(int staffId, DateTime date)
    {
        return await _dbSet.Where(a => a.StaffId == staffId && a.StartTime.Date == date.Date).ToListAsync();
    }
    public async Task<List<DailyServiceDurationDto>> GetDailyServiceDurationsByStaffIdAsync(
        int staffId,
        DateTime startDate,
        DateTime endDate,
        string timeZoneId)
    {
        TimeZoneInfo userLocalTimeZone;
        try
        {
            userLocalTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        catch (TimeZoneNotFoundException)
        {
            // Örnek: Sunucunun yerel saat dilimini kullan (dikkatli olun, bu istenen davranış olmayabilir)
            // userLocalTimeZone = TimeZoneInfo.Local;
            throw new ArgumentException($"Belirtilen saat dilimi ID'si '{timeZoneId}' bulunamadı.", nameof(timeZoneId));
        }
        catch (InvalidTimeZoneException)
        {
            throw new ArgumentException($"Belirtilen saat dilimi ID'si '{timeZoneId}' geçersiz.", nameof(timeZoneId));
        }


        // startDate ve endDate'in günün başlangıcı (00:00:00) olduğunu varsayıyoruz.
        // DateTimeKind.Unspecified olarak işaretleyerek, TimeZoneInfo dönüşümlerinin doğru çalışmasını sağlıyoruz.
        var localStartDateTime = DateTime.SpecifyKind(startDate.Date, DateTimeKind.Unspecified);
        var localEndDateTimeBoundary = DateTime.SpecifyKind(endDate.Date.AddDays(1), DateTimeKind.Unspecified);

        // Bu yerel başlangıç ve bitiş noktalarını UTC'ye dönüştür (veritabanındaki StartTime'ın UTC olduğu varsayımıyla)
        var utcStartDate = TimeZoneInfo.ConvertTimeToUtc(localStartDateTime, userLocalTimeZone);
        var utcEndDateBoundary = TimeZoneInfo.ConvertTimeToUtc(localEndDateTimeBoundary, userLocalTimeZone);
        
        var appointmentsInUtc = await _dbSet 
            .Where(a => a.StaffId == staffId &&
                        a.StartTime >= utcStartDate &&
                        a.StartTime < utcEndDateBoundary) 
            .ToListAsync(); 

        // Şimdi çekilen verileri bellekte kullanıcının yerel saat dilimine göre işle
        var result = appointmentsInUtc
            .Select(appointment => new
            {
                OriginalAppointment = appointment,
                // Her bir randevunun StartTime değerini UTC'den kullanıcının yerel saat dilimine dönüştür
                LocalStartTime = TimeZoneInfo.ConvertTimeFromUtc(appointment.StartTime, userLocalTimeZone)
            })
            .GroupBy(appointmentWithLocalTime => appointmentWithLocalTime.LocalStartTime.Date) 
            .Select(group => new DailyServiceDurationDto
            {
                StaffId = staffId, 
                Date = group.Key,  // group.Key, gruplamanın yapıldığı yerel tarihtir
                TotalDurationMinutes = group.Sum(x => x.OriginalAppointment.Duration.TotalMinutes),
                Appointments = group.Select(x => new TimeRange(
                    x.LocalStartTime.TimeOfDay, // Dönüştürülmüş LocalStartTime'ın TimeOfDay'ı
                    x.LocalStartTime.TimeOfDay + x.OriginalAppointment.Duration 
                )).ToList()
            })
            .OrderBy(dto => dto.Date) 
            .ToList();

        return result;
    }
    
}
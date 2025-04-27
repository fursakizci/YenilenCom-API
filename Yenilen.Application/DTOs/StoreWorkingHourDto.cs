namespace Yenilen.Application.DTOs;

public class StoreWorkingHourDto
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeSpan? OpeningTime { get; set; } // "10:00"
    public TimeSpan? ClosingTime { get; set; } // "19:00"
    public bool IsClosed { get; set; }
}
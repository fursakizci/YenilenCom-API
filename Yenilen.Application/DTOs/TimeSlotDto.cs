namespace Yenilen.Application.DTOs;

public class TimeSlotDto
{
    public int StartTimeInSeconds { get; set; } // 32400
    public string FormattedTime { get; set; } // "09:00 AM"
    public int Duration { get; set; } // 30
    public int StaffId { get; set; }
}
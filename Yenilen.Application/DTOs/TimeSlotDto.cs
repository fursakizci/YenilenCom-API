namespace Yenilen.Application.DTOs;

public class TimeSlotDto
{
    public int StartTimeInSeconds { get; set; }
    public string FormattedTime { get; set; } 
    public TimeSpan AvailableSlut { get; set; }
    public TimeSpan Duration { get; set; }
}
namespace Yenilen.Application.DTOs;

public class TimeSlotDto
{
    public int StartTimeInSeconds { get; set; }
    public string FormattedTime { get; set; } 
    public int Duration { get; set; }
}
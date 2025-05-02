namespace Yenilen.Application.DTOs;

public class AvailableDateDto
{
    
    public DateTime Date { get; set; } // 2024-04-12
    public string DayName { get; set; } // sat
    public string Year { get; set; } //2025
    public string Month { get; set; } // april
    public string DayOfMonth { get; set; } // 12
    public List<TimeSlotDto> TimeSlots { get; set; }
}
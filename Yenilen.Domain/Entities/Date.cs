namespace Yenilen.Domain.Entities;

public class Date
{
    public DateTime DateTime { get; set; }
    public TimeSpan Time { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}
using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Date:BaseEntity
{
    public DateTime DateTime { get; set; }
    public TimeSpan Time { get; set; }
    public DayOfWeek DayOfWeek { get; set; }
}
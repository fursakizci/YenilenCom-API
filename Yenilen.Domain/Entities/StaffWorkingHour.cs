using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class StaffWorkingHour : BaseEntity
{
    public int StaffId { get; set; }
    public Staff Staff { get; set; }

    public DayOfWeek DayOfWeek { get; set; }              

    public TimeSpan StartTime { get; set; }              
    public TimeSpan EndTime { get; set; }                 

    public bool IsClosed { get; set; } = false;          
}
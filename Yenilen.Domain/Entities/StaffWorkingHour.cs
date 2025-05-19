using System.ComponentModel.DataAnnotations.Schema;
using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class StaffWorkingHour : BaseEntity
{
    public int StaffId { get; set; }
    public Staff Staff { get; set; }

    public DayOfWeek DayOfWeek { get; set; }              

    public TimeSpan StartTime { get; set; }
    [NotMapped]
    public int StartTimeInSecond => (int)StartTime.TotalSeconds;
    public TimeSpan EndTime { get; set; }
    [NotMapped]
    public int EndTimeInSecond => (int)EndTime.TotalSeconds;

    public bool IsClosed { get; set; } = false;          
}
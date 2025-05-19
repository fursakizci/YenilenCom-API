using System.ComponentModel.DataAnnotations.Schema;
using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class StoreWorkingHour:BaseEntity
{
    public int StoreId { get; set; }
    public Store Store { get; set; }

    public DayOfWeek DayOfWeek { get; set; }        
    public TimeSpan OpeningTime { get; set; }
    [NotMapped]
    public int OpeningTimeInSecond => (int)OpeningTime.TotalSeconds;
    public TimeSpan ClosingTime { get; set; }
    [NotMapped]
    public int ClosingTimeInSecond => (int)ClosingTime.TotalSeconds;
    public bool IsClosed { get; set; } = false;
}
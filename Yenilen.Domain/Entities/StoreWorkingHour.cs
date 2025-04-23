using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class StoreWorkingHour:BaseEntity
{
    public int StoreId { get; set; }
    public Store Store { get; set; }

    public DayOfWeek DayOfWeek { get; set; }        
    public TimeSpan OpeningTime { get; set; }       
    public TimeSpan ClosingTime { get; set; }      

    public bool IsClosed { get; set; } = false;
}
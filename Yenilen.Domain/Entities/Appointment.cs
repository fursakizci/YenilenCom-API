using System.ComponentModel.DataAnnotations.Schema;
using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Domain.Entities;

public class Appointment:BaseEntity
{
    
    public int? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public int StaffId { get; set; }
    public Staff Staff { get; set; }
    
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    
    [NotMapped]
    public int DurationInSeconds => (int)Duration.TotalSeconds;
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public bool IsPaid { get; set; } = false;
    public string? Note { get; set; }
}
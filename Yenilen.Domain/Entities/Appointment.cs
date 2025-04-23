using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Domain.Entities;

public class Appointment:BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    // public int CategoryId { get; set; }
    // public Category Category { get; set; }
    // public int ServiceId { get; set; }
    // public Service Service { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Service> Services { get; set; } = new List<Service>();
    public int StaffId { get; set; }
    public Staff Staff { get; set; }
  
    public DateTime StartTime { get; set; }
    public TimeSpan Duration { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pending;
    public bool IsPaid { get; set; } = false;
    public string? Note { get; set; }
}
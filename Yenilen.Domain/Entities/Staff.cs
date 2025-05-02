using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Staff:BaseEntity
{
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public string Name { get; set; }
    
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? Bio { get; set; }
    
    public int ImageId { get; set; }
    public Image? Image { get; set; }
    public ICollection<StaffWorkingHour> WorkingHours { get; set; } = new List<StaffWorkingHour>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
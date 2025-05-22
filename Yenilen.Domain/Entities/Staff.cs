using Yenilen.Domain.Common;
using Yenilen.Domain.Users;

namespace Yenilen.Domain.Entities;

public class Staff:BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public string FullName => $"{FirstName} {LastName}"; 
    
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public bool IsAvailable { get; set; } = true;
    public string? Bio { get; set; }
    
    public int? ImageId { get; set; }
    public Image? Image { get; set; }
    public ICollection<StaffWorkingHour> WorkingHours { get; set; } = new List<StaffWorkingHour>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Store:BaseEntity
{
    // StoreOwner ile bire-bir ilişki
    public int StoreOwnerId { get; set; }
    public StoreOwner StoreOwner { get; set; }
    
    public string? StoreName { get; set; }
    public string? ManagerName { get; set; }
    public string? ManagerPhone { get; set; }
    public int? AddressId { get; set; }
    public Address? Address { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? About { get; set; }

    public bool IsConfirm { get; set; } = false;
    
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
    public ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();
    public ICollection<StoreWorkingHour> WorkingHours { get; set; } = new List<StoreWorkingHour>();
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
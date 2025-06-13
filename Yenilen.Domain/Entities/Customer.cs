using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;
using Yenilen.Domain.Users;

namespace Yenilen.Domain.Entities;

public class Customer:BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public Image? AvatarUrl { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
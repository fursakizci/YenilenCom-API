using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Domain.Entities;

public class User:BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public Image? AvatarUrl { get; set; }
    public ICollection<Address> Addresses { get; set; } = new List<Address>();
    public ICollection<Favourite> Favourites { get; set; } = new List<Favourite>();
    
    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    
}
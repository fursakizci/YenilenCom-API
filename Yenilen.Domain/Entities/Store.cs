using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Store:BaseEntity
{
    public string Name { get; set; }
    public int AddressId { get; set; }
    public Address Address { get; set; }
    public string? MobileNumber { get; set; }
    public string? PhoneNumber { get; set; }
    public string? About { get; set; }
    
    public int FavouriteId { get; set; }
    public Favourite Favourite { get; set; }
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    //public ICollection<Service> Services { get; set; } = new List<Service>();
    public ICollection<Staff> StaffMembers { get; set; } = new List<Staff>();
    public ICollection<StoreWorkingHour> WorkingHours { get; set; } = new List<StoreWorkingHour>();
    public ICollection<Image> Images { get; set; } = new List<Image>();
    public ICollection<Tag> Tags { get; set; } = new List<Tag>();
}
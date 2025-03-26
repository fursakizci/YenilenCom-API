using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Store:BaseEntity
{
    public string Name { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    public string LocalPhoneNumber { get; set; }
    public string About { get; set; }
    public ICollection<Category> Categories { get; set; }
    public ICollection<Service> Services { get; set; }
    public ICollection<Staff> Staffes { get; set; }
    public ICollection<Date> OpeningTimes { get; set; }
    public ICollection<Image> Images { get; set; }
    public ICollection<Tag> Tags { get; set; }
}
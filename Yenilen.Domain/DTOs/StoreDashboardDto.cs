using Yenilen.Domain.Entities;

namespace Yenilen.Domain.DTOs;

public class StoreDashboardDto
{
    public int Id { get; set; }
    public string StoreName { get; set; }
    public Address Address { get; set; }
    public string About { get; set; }
    public IEnumerable<Image> Images { get; set; }
    public IEnumerable<Category> Categories { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
    public IEnumerable<StoreWorkingHour> StoreWorkingHours { get; set; }
    public IEnumerable<Staff> StaffMembers { get; set; }
}
using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; }
    public ICollection<Service> Services { get; set; } = new List<Service>();
}
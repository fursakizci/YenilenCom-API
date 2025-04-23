using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Tag:BaseEntity
{
    public string Name { get; set; }
    public ICollection<Store> Stores { get; set; } = new List<Store>();
}
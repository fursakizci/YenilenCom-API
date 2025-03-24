using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Service:BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public Category Category { get; set; }
}
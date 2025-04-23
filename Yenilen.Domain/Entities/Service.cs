using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;

namespace Yenilen.Domain.Entities;

public class Service:BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public CurrencyType Currency { get; set; }
    public TimeSpan Duration { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
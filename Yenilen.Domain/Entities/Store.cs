using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Store:BaseEntity
{
    public string Name { get; set; }
    public string About { get; set; }
}
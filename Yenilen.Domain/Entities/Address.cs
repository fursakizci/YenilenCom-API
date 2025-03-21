using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Address:BaseEntity
{
    public string Name { get; set; }
    public float longitude { get; set; }
    public float latitude { get; set; }
}
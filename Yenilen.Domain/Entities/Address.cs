using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Address:BaseEntity
{
    public string Country { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
    public string FullAddress { get; set; }
    public float longitude { get; set; }
    public float latitude { get; set; }
}
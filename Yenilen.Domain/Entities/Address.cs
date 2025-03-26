using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class Address:BaseEntity
{
    public string Label { get; set; }
    public string Region { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Town { get; set; }
    public string District { get; set; }
    public string FullAddress { get; set; }
    public double longitude { get; set; }
    public double latitude { get; set; }
}
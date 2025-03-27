namespace Yenilen.Application.DTOs;

public class AddressDto
{
    public string Id { get; set; }
    public string Label { get; set; }
    public string FullAddress { get; set; }
    public double longitude { get; set; }
    public double latitude { get; set; }
    public string CountryCode { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string District { get; set; }
    public string Neighbourhood { get; set; }
    public string Region { get; set; }
    public string PostCode { get; set; }
}
namespace Yenilen.Application.DTOs;

public class StoreDto
{
    public int StoreId { get; set; }
    public string? Name { get; set; }
    public double? Rating  { get; set; }
    public int CountOfReview { get; set; }
    public List<string>? ImageUrl { get; set; }
    public double Distance { get; set; }
    public AddressDto Address { get; set; }
    public List<ServiceDto> Services { get; set; }
}
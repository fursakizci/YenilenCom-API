namespace Yenilen.Domain.DTOs;

public class StoreSearchResultDto
{
    public string StoreId { get; set; }
    public string? Name { get; set; }
    public double? Rating { get; set; }
    public int CountOfReview { get; set; }
    public string? ImageUrl { get; set; } // Veya List<string> ImageUrls
    public double Distance { get; set; }
    public string FullAddress { get; set; }
    
    public double Longitude { get; set; }
    public double Latitude { get; set; }

    public List<ServiceDto> Services { get; set; }
}

public class ServiceDto
{
    public string CategoryId { get; set; }
    public string ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public TimeSpan Duration { get; set; }
}
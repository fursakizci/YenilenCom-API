namespace Yenilen.Application.DTOs;

public class StoreDto
{
    public string? Name { get; set; }
    public string? WebsiteUrl { get; set; }
    public List<int>? TagIds { get; set; }
    public string? CountOfWorkers { get; set; }
    public string? OwnerName { get; set; }
    public ICollection<StoreWorkingHourDto>? StoreWorkingHours { get; set; }
}
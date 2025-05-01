namespace Yenilen.Application.DTOs;

public class ServiceDto
{
    public string CategoryId { get; set; }
    public int ServiceId { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Duration { get; set; }
    public int MaxInSecond { get; set; } = 0;
    public int MinInSecond { get; set; } = 0;
}
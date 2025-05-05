namespace Yenilen.Application.DTOs;

public class StoreIndividualDto
{
    public string Name { get; set; }
    public AddressDto Address { get; set; }
    public List<ImageDto> Images { get; set; }
    public List<StoreWorkingHourDto> StoreWorkingHours { get; set; }
    public decimal Rating { get; set; }
    public List<ReviewDto> Reviews { get; set; }
    public List<CategoryDto> Categories { get; set; }
    public List<StaffDto> StaffMembers { get; set; }
    public string? About { get; set; }
    
}
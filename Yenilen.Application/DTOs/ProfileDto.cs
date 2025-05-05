namespace Yenilen.Application.DTOs;

public class ProfileDto
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Initials { get; set; }
    public string MobileNumber { get; set; }
    public string Email { get; set; }
    public string DateOfBirth { get; set; }
    public string Gender { get; set; }
    public IReadOnlyList<AddressDto> Addresses { get; set; }
    public ImageDto Image { get; set; }
}
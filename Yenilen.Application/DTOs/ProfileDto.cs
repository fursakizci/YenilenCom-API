namespace Yenilen.Application.DTOs;

public class ProfileDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string DateOfBirth { get; set; }
    public string Sex { get; set; }
    public IReadOnlyList<AddressDto> Address { get; set; }
    public ImageDto Image { get; set; }
}
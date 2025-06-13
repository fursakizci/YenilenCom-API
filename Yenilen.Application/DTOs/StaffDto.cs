namespace Yenilen.Application.DTOs;

public class StaffDto
{
    public int Id { get; set; }
    public int StoreId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? BirthOfDate { get; set; }
    public string StartDate { get; set; }
    public string? Bio { get; set; }
    public string ImageUrl { get; set; }
}
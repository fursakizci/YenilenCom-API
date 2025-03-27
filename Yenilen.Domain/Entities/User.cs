using Yenilen.Domain.Common;

namespace Yenilen.Domain.Entities;

public class User:BaseEntity
{
    public string FirstName { get; set; }
    public string Surname { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Sex { get; set; }
    public ICollection<Address>? Addresses { get; set; }
    public ICollection<Favourite> Favourites { get; set; }
    public Image? Image { get; set; }
}
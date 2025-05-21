using Yenilen.Domain.Common;
using Yenilen.Domain.Common.Enums;
using Yenilen.Domain.Users;

namespace Yenilen.Domain.Entities;

public class StoreOwner : BaseEntity
{
    // Identity ilişkisi
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    
    // Kişisel bilgiler
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    
    // İş bilgileri
    public string? CompanyName { get; set; }
    public string? TaxNumber { get; set; }
    public string? CompanyRegistrationNumber { get; set; }
    
    // Profil
    public int? ImageId { get; set; }
    public Image? ProfileImage { get; set; }
    
    // Store - bire bir ilişki
    public Store Store { get; set; }
}
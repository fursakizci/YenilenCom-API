using Microsoft.AspNetCore.Identity;

namespace Yenilen.Domain.Users;

public sealed class AppUser : IdentityUser<Guid>
{

    public AppUser()
    {
        Id = Guid.NewGuid();
    }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    
    #region Audit Log

    public Guid CreateUserId { get; set; } = default!;
    public Guid? UpdateUserId { get; set; }
    public Guid DeleteUserId { get; set; }
    
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteAt { get; set; }

    #endregion
}
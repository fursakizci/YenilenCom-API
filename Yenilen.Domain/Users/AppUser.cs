using Microsoft.AspNetCore.Identity;
using Yenilen.Domain.Entities;

namespace Yenilen.Domain.Users;

public sealed class AppUser : IdentityUser<Guid>
{
    public AppUser()
    {
        Id = Guid.NewGuid();
        IsActive = true;
        IsDeleted = false;
        CreatedAt = DateTime.UtcNow;
    }
    public Guid RoleId { get; set; }
    public AppRole Role { get; set; } = default!;
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

    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

    #endregion
}
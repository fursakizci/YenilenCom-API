using Yenilen.Domain.Common;
using Yenilen.Domain.Users;

namespace Yenilen.Domain.Entities;

public class RefreshToken:BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public string Token { get; set; } = default!;
    public DateTime Expires { get; set; }
    public bool IsRevoked { get; set; } = false;
    public DateTime? RevokedAt { get; set; }
    public string? CreatedByIp { get; set; }

    public bool IsExpired => DateTime.UtcNow >= Expires;
    public bool IsActive => !IsRevoked && !IsExpired;
}
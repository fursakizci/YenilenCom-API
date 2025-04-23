namespace Yenilen.Domain.Common;

public class BaseEntity
{
    public BaseEntity()
    {
        Uuid = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        IsActive = true;
        IsDeleted = false;
    }
    public int Id { get; set; }
    public Guid Uuid { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeleteAt { get; set; }
}